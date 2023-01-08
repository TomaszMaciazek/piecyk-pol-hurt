namespace PiecykPolHurt.Model.Entities
{
    public class OrderLines
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int ItemsQuantity { get; set; }
        public decimal PriceForOneItem { get; set; }
    }
}
