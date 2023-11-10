namespace Domain
{
    public class Location
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}