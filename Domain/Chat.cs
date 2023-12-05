namespace Domain
{
    public class Chat
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public AppUser Sender { get; set; }
        public AppUser Recipient { get; set; }
        public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;
    }
}