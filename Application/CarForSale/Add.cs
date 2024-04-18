
using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CarForSale
{
    public class Add
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AddCarForSaleDTO Object { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Object).SetValidator(new CarForSaleValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _Context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _Context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _Context.Users.Include(u => u.Cars).FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user == null) return null;

                var car = await _Context.Cars.FindAsync(request.Object.CarId);
                if (car == null) return null;

                if (car.AppUserId != user.Id) return Result<Unit>.Unauthorized();

                car.Price = request.Object.Price;
                var carForSaleExists = await _Context.CarsForSale.AnyAsync(x => x.Car.Id == request.Object.CarId);
                if (carForSaleExists) return Result<Unit>.Failure("Car is already for sale");

                var newLocation = new Location
                {
                    Latitude = request.Object.Location.Latitude,
                    Longitude = request.Object.Location.Longitude,
                };

                var carForSale = new Domain.CarForSale
                {
                    Car = car,
                    Description = request.Object.Description,
                    Date = DateTime.Now,
                    Location = newLocation,
                    Contact = request.Object.Contact,
                    Engine = request.Object.Engine,
                    Transmission = request.Object.Transmission,
                    StartingBid = request.Object.StartingBid,
                    HighestBid = request.Object.StartingBid,
                };

                _Context.CarsForSale.Add(carForSale);



                var result = await _Context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Failed to add car for sale");

                return Result<Unit>.Success(Unit.Value);

            }
        }

    }
}