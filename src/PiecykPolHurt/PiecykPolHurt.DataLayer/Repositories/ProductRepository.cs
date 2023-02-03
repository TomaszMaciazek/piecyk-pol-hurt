using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public interface IProductRepository : IRepository<Product> {
        Task<bool> WasProductUsedInOrder(int id);
        Task<Product> GetByCode(String code);
    }
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> WasProductUsedInOrder(int id)
            => await _context.OrdersLines.AnyAsync(x => x.ProductId == id);

        public async Task<Product> GetByCode(String code)
            => await _context.Products.FirstAsync(product => product.Code.Equals(code));

    }
}
