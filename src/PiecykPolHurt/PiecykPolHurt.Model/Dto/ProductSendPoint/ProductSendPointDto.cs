using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.Model.Dto.ProductSendPoint;

public class ProductSendPointDto
{
    public int ProductId { get; set; }
    public int SendPointId { get; set; }
    public int AvailableQuantity { get; set; }
    public DateTime ForDate { get; set; }
}