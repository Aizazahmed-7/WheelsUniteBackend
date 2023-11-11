using Domain;
using FluentValidation;

namespace Application.Events
{
    public class EventValidator : AbstractValidator<Event>
    {

        public EventValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Location).NotNull().SetValidator(new LocationValidator());

        }


    }

    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(x => x.Latitude).NotEmpty();
            RuleFor(x => x.Longitude).NotEmpty();
        }
    }

}