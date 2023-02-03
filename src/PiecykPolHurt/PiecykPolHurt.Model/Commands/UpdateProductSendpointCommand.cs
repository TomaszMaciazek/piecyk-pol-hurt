using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.Model.Commands;

public class UpdateProductSendpointCommand
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int SendPointId { get; set; }
    public SendPoint SendPoint { get; set; }
    public int AvailableQuantity { get; set; }
    public DateTime ForDate { get; set; }
}