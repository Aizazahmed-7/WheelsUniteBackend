
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
        public int StartingBid { get; set; }
        public string Transmission { get; set; }
        public string Engine { get; set; }
        public string Contact { get; set; }
        public int HighestBid { get; set; }
        public Guid HighestBidderId { get; set; }
    }
}