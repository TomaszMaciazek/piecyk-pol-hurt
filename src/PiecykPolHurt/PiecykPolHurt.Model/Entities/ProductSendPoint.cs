namespace PiecykPolHurt.Model.Entities
{
    public class ProductSendPoint : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SendPointId { get; set; }
        public SendPoint SendPoint { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
