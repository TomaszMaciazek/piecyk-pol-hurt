namespace PiecykPolHurt.DataLayer.Repositories
{
    using PiecykPolHurt.Model.Entities;

    public interface IProductRepository : IReadOnlyRepository<Product> { }
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
