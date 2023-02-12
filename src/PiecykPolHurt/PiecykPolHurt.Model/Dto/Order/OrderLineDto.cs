namespace PiecykPolHurt.Model.Dto.Order
{
    public class OrderLineDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int ItemsQuantity { get; set; }
        public decimal PriceForOneItem { get; set; }
    }
}
