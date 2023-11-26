
namespace Domain
{
    public class CarForSale
    {
        public Guid Id { get; set; }
        public Car Car { get; set; }
        public DateTime Date { get; set; } // DateTime is a struct, so it's not nullable
        public string Description { get; set; }
        public Location Location { get; set; }
        public bool IsSold { get; set; } // default value is false

    }
}