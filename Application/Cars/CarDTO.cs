

using Domain;

namespace Application.Cars
{
    public class CarDTO
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
        public int Price { get; set; }
        public string RegisteredIn { get; set; }
        public string ConditionDetails { get; set; }
        public int Mileage { get; set; }
        public string AppUserId { get; set; }
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public bool IsMain { get; set; }
    }
}