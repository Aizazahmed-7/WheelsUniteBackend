using Application.Events;

namespace Application.Events
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; } // DateTime is a s truct, so it's not nullable
        public string Description { get; set; }
        public string HostUsername { get; set; }
        public string IsCancelled { get; set; } // default value is false
        public ICollection<AttendeeDto> Attendees { get; set; }
        public LocationDto Location { get; set; }
    }
}