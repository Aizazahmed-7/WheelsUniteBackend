
using Domain;

namespace Application.CarForSale
{
    public class CarForSaleDetailsDTO
    {
        public Guid Id { get; set; }
        public Car Car { get; set; }
        public Location Location { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

    
    }
}