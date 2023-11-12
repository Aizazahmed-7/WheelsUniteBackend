namespace Domain
{
    public class Like
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public virtual Post Post { get; set; }
        public Guid PostId { get; set; }
    }
}