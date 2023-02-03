namespace PiecykPolHurt.Model.Commands
{
    public class CreateOrderLineCommand
    {
        public int ProductId { get; set; }
        public int ItemsQuantity { get; set; }
        public decimal PriceForOneItem { get; set; }
    }
}
