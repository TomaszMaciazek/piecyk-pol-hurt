using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class SendPoint : BaseEntity
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public double Logitude { get; set; }
        public double Latitude { get; set; }
        public ICollection<ProductSendPoint> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
