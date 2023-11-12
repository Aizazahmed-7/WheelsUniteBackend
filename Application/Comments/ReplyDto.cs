

namespace Application.Comments
{
    public class ReplyDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}