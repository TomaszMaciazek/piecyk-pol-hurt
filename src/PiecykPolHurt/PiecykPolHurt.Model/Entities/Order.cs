using PiecykPolHurt.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class Order : AuditableEntity
    {
        [Required]
        public DateTime RequestedReceptionDate { get; set; }

        public DateTime? ReceptionDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

    }
}
