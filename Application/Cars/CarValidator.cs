using API.DTOs;
using FluentValidation;

namespace Application.Cars
{
    public class CarValidator : AbstractValidator<AddCarDTO>
    {

        public CarValidator()
        {
            RuleFor(x => x.Make).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
        }
    }
}