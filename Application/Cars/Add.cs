
using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cars
{
    public class Add
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Car car { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.car).SetValidator(new CarValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _Context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _Context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _Context.Users.Include(u => u.Cars).FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user == null) return null;

                var car = new Car
                {
                    Make = request.car.Make,
                    Model = request.car.Model,
                    Color = request.car.Color,
                    ConditionDetails = request.car.ConditionDetails,
                    Mileage = request.car.Mileage,
                    Price = request.car.Price,
                };

                if (user.Cars.Count == 0)
                {
                    car.IsMain = true;
                }
                else
                {
                    car.IsMain = false;
                }

                user.Cars.Add(car);

                var result = await _Context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to add car");

                return Result<Unit>.Success(Unit.Value);
            }


        }
    }
}