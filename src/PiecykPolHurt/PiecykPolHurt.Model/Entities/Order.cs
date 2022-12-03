using PiecykPolHurt.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
