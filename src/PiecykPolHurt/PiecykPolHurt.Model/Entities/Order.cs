namespace PiecykPolHurt.Model.Entities
{
    using PiecykPolHurt.Model.Enums;
    using System.ComponentModel.DataAnnotations;

    public class Order : AuditableEntity
    {
        [Required]
        public int BuyerId { get; set; }

        public User Buyer { get; set; }

        [Required]
        public DateTime RequestedReceptionDate { get; set; }

        public DateTime? ReceptionDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public ICollection<OrderLines> Lines { get; set; }
    }
}
