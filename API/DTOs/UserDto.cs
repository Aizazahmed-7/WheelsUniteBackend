
using Application.Posts;

namespace API.DTOs
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public int PostsCount { get; set; }
    }
}