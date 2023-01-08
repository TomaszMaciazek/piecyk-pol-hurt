namespace PiecykPolHurt.Model.Entities
{
    public class SendPoint : BaseEntity
    {
        public string Code { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ProductSendPoint> Products { get; set; }
    }
}
