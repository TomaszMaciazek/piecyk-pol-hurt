using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public interface IProductRepository : IRepository<Product> {
        Task<bool> WasProductUsedInOrder(int id);
        Task<Product> GetByCode(string code);
    }
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> WasProductUsedInOrder(int id)
            => await _context.OrdersLines.AnyAsync(x => x.ProductId == id);

        public async Task<Product> GetByCode(string code)
            => await _context.Products.FirstOrDefaultAsync(product => product.Code.Equals(code));

    }
}
