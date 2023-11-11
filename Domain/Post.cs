

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}