
namespace Domain
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; } // DateTime is a struct, so it's not nullable
        public string Description { get; set; }
        public Location Location { get; set; }
        public Photo Photo { get; set; }
        public bool IsCancelled { get; set; } // default value is false        
        public ICollection<EventAttendee> Attendees { get; set; } = new List<EventAttendee>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();


    }
}