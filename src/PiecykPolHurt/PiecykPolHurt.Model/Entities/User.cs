namespace PiecykPolHurt.Model.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
