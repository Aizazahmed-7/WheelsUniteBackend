using Application.Events;
using Domain;

namespace Application.CarForSale
{
    public class AddCarForSaleDTO
    {
        public Guid CarId { get; set; }
        public string Description { get; set; }
        public LocationDto Location { get; set; }
    }
}