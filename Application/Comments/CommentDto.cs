namespace Application.Comments
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string CreatedAt { get; set; }
        public ICollection<ReplyDto> Replies { get; set; }
    }
}