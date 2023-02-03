using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class Product : AuditableEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public ICollection<OrderLine> Lines { get; set; }
        public ICollection<ProductSendPoint> SendPoints { get; set; }
    }
}
