
using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cars
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Car car { get; set; }
        }


        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.car).SetValidator(new UpdateCarValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _Context;
            private readonly IUserAccessor _UserAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _Context = context;
                _UserAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var car = await _Context.Cars.FindAsync(request.car.Id);
                if (car == null) return null;
                var user = await _Context.Users.FirstOrDefaultAsync(x => x.UserName == _UserAccessor.GetUsername());
                if (user == null) return null;


                if (car.AppUserId != user.Id) return Result<Unit>.Failure("You are not authorized to edit this car");

                car.ConditionDetails = request.car.ConditionDetails ?? car.ConditionDetails;
                car.Mileage = request.car.Mileage == 0 ? car.Mileage : request.car.Mileage;
                car.Price = request.car.Price == 0 ? car.Price : request.car.Price; ;
                car.Color = request.car.Color ?? car.Color;



                var result = await _Context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update car");

                return Result<Unit>.Success(Unit.Value);
            }
        }

    }
}