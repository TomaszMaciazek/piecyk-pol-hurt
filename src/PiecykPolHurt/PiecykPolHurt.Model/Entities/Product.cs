namespace PiecykPolHurt.Model.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Product : AuditableEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<OrderLines> Lines { get; set; }

        public ICollection<ProductSendPoint> SendPoints { get; set; }
    }
}
