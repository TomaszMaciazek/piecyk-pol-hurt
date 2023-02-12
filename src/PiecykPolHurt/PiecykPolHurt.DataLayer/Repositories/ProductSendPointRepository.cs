using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public interface IProductSendPointRepository : IRepository<ProductSendPoint> { }

    public class ProductSendPointRepository : BaseRepository<ProductSendPoint>, IProductSendPointRepository
    {
        public ProductSendPointRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}