
using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            // public Event Event { get; set; }
            public string Title { get; set; }
            public DateTime Date { get; set; } // DateTime is a struct, so it's not nullable
            public string Description { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public IFormFile File { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Date).NotEmpty();
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Latitude).NotEmpty();
                RuleFor(x => x.Longitude).NotEmpty();
                RuleFor(x => x.File).NotEmpty();

            }
        }
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext context;

            public readonly IUserAccessor userAccessor;

            private readonly IPhotoAccessor photoAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor , IPhotoAccessor photoAccessor)
            {
                this.context = context;
                this.photoAccessor = photoAccessor;
                this.userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());

                if(user == null) return null;

                var photoUploadResult = await this.photoAccessor.AddPhoto(request.File);

                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId,
                    IsMain = true
                };

                var Event = new Event
                {
                    Title = request.Title,
                    Date = request.Date,
                    Description = request.Description,
                    Location = new Location
                    {
                        Latitude = request.Latitude,
                        Longitude = request.Longitude
                    },
                    Photo = photo
                    
                };

                var attendee = new EventAttendee
                {
                    AppUser = user,
                    Event = Event,
                    IsHost = true
                };

                Event.Attendees.Add(attendee);

                this.context.Events.Add(Event);
                var result = await this.context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return Result<Unit>.Failure("Failed to create Event");
                }
                return Result<Unit>.Success(Unit.Value);

            }

        }
    }
}