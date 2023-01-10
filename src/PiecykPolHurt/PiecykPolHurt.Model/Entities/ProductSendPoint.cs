using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class ProductSendPoint : BaseEntity
    {
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int SendPointId { get; set; }
        public SendPoint SendPoint { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
    }
}
