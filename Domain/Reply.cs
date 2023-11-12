
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Reply
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        // Foreign key for the Comment
        public Guid CommentId { get; set; }

        // Navigation property for the Comment
        public virtual Comment Comment { get; set; }

        // Foreign key for the User who created the reply
        public string AppUserId { get; set; }

        // Navigation property for the User
        public virtual AppUser Author { get; set; }
    }
}