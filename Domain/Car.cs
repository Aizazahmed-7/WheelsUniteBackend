
namespace Domain
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public int Price { get; set; }
        public string ConditionDetails { get; set; }
        public int Mileage { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsMain { get; set; }
    }
}