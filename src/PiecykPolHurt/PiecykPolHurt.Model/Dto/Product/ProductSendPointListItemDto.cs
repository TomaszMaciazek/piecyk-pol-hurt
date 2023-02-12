namespace PiecykPolHurt.Model.Dto.Product
{
    public class ProductSendPointListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
