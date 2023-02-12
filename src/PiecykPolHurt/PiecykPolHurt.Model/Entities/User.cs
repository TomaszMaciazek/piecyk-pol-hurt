using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class User : BaseEntity
    {

        [Required]
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
