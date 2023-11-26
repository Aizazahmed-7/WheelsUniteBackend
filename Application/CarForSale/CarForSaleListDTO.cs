
using Application.Events;

namespace Application.CarForSale
{
    public class CarForSaleListDTO
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public LocationDto Location { get; set; }
    }
}