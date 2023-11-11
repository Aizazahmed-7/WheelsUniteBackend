

using Domain;

namespace Application.Posts
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string Username { get; set; }
        public int LikesCount { get; set; }
        public List<Photo> Photos { get; set; }
        public bool IsLiked { get; set; }

    }
}