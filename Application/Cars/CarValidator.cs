using Domain;
using FluentValidation;

namespace Application.Cars
{
    public class CarValidator : AbstractValidator<Car>
    {

        public CarValidator()
        {
            RuleFor(x => x.Make).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
        }
    }
}