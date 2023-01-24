using PiecykPolHurt.DataLayer.Repositories;

namespace PiecykPolHurt.DataLayer.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository ProductRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public ISendPointRepository SendPointRepository { get; private set; }
        public IDictionaryValueRepository DictionaryValueRepository { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(_context);
            OrderRepository = new OrderRepository(_context);
            SendPointRepository = new SendPointRepository(_context);
            DictionaryValueRepository = new DictionaryValueRepository(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
