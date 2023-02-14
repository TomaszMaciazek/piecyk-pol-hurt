using PiecykPolHurt.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class NotificationTemplate : BaseEntity
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public NotificationType Type { get; set; }
    }
}
