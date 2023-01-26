namespace PiecykPolHurt.Model.Entities;

public class ProductsStock : BaseEntity
{
    public Product Product { get; set; }
    public decimal Quantity { get; set; }
    public string Branch { get; set; }
}