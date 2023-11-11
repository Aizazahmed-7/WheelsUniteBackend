
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Like
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public virtual Post Post { get; set; }
    }
}