using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        public string Email { get; set; }
    }
}
