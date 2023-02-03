using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.Model.Commands;

public class CreateProductSendPointCommand
{
    public Product Product { get; set; }
    public int SendPointId { get; set; }
    public SendPoint SendPoint { get; set; }
    public int AvailableQuantity { get; set; }
    public DateTime ForDate { get; set; }
}