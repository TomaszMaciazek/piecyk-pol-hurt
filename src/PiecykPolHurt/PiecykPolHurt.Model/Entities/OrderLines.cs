namespace PiecykPolHurt.Model.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class OrderLines : BaseEntity
    {
        public Order Order { get; set; }

        public Product Product { get; set; }

        [Required]
        public int ItemsQuantity { get; set; }

        [Required]
        public decimal PriceForOneItem { get; set; }
    }
}
