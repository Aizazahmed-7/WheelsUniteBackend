namespace Domain
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public virtual AppUser Author { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Foreign key for the Post 
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();
    }
}