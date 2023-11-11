using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using FluentValidation;

namespace Application.Cars
{
    public class UpdateCarValidator : AbstractValidator<Car>
    {
        public UpdateCarValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}