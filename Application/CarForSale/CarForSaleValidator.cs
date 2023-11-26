using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Events;
using Domain;
using FluentValidation;

namespace Application.CarForSale
{
    public class CarForSaleValidator : AbstractValidator<AddCarForSaleDTO>
    {
        public CarForSaleValidator()
        {
            RuleFor(x => x.CarId).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Location).NotNull().SetValidator(new LocationValidator());
        }
    }

       public class LocationValidator : AbstractValidator<LocationDto>
    {
        public LocationValidator()
        {
            RuleFor(x => x.Latitude).NotEmpty();
            RuleFor(x => x.Longitude).NotEmpty();
        }
    }
}