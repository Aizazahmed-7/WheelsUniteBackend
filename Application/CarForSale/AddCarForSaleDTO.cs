using Application.Events;
using Domain;

namespace Application.CarForSale
{
    public class AddCarForSaleDTO
    {
        public Guid CarId { get; set; }
        public string Description { get; set; }
        public LocationDto Location { get; set; }
        public int Price { get; set; }
        public int StartingBid { get; set; }
        public string Transmission { get; set; }
        public string Engine { get; set; }
        public string Contact { get; set; }


    }
}