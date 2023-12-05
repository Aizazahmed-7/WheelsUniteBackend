namespace Application.Chats
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SenderImage { get; set; }
        public string RecipientImage { get; set; }
    }
}