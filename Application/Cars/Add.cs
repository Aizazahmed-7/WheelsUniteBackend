using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cars
{
    public class Add
    {
        public class Command : IRequest<Result<Unit>>
        {
            // public AddCarDTO car { get; set; }
            public IFormFile File { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
            public string Color { get; set; }
            public int Mileage { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                // RuleFor(x => x.car).SetValidator(new CarValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _Context;
            private readonly IUserAccessor _userAccessor;
            private readonly IPhotoAccessor _photoAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _Context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _Context.Users.Include(u => u.Cars).FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user == null) return null;

                var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId,
                    IsMain = true
                };

                var car = new Car
                {
                    Mileage = request.Mileage,
                    Make = request.Make,
                    Model = request.Model,
                    Color = request.Color,
                    Photos = new List<Photo> { photo }
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