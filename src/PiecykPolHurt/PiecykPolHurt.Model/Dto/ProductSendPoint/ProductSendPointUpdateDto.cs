namespace PiecykPolHurt.Model.Dto.ProductSendPoint;

public class ProductSendPointUpdateDto
{
    public ProductSendPointUpdateDto(string productCode, string sendPointCode, int availableQuantity, DateTime forDate, decimal price)
    {
        ProductCode = productCode;
        SendPointCode = sendPointCode;
        AvailableQuantity = availableQuantity;
        ForDate = forDate;
        Price = price;
    }

    public String ProductCode { get; set; }
    public String SendPointCode { get; set; }
    public int AvailableQuantity { get; set; }
    public DateTime ForDate { get; set; }
    public decimal Price { get; set; }
}