using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class OrderLine : BaseEntity
    {
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int ItemsQuantity { get; set; }
        [Required]
        public decimal PriceForOneItem { get; set; }
    }
}
