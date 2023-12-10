namespace Application.Chats
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public string CreatedAt { get; set; }
        public string SenderImage { get; set; }
        public string RecipientImage { get; set; }
        public string ConversationId { get; set; }
    }
}