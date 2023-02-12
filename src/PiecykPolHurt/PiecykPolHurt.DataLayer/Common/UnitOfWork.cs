using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PiecykPolHurt.DataLayer.Repositories;
using System.Data;

namespace PiecykPolHurt.DataLayer.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProductRepository ProductRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public ISendPointRepository SendPointRepository { get; private set; }
        public IDictionaryValueRepository DictionaryValueRepository { get; set; }
        public IReportDefinitionRepository ReportDefinitionRepository { get; set; }
        public IProductSendPointRepository ProductSendPointRepository { get; set; }

        public string ConnectionString { get => 
                !string.IsNullOrEmpty(_context.Database.GetConnectionString())
                ? _context.Database.GetConnectionString()
                : throw new EmptyConnectionStringException(); }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(_context);
            OrderRepository = new OrderRepository(_context);
            SendPointRepository = new SendPointRepository(_context);
            DictionaryValueRepository = new DictionaryValueRepository(_context);
            ReportDefinitionRepository = new ReportDefinitionRepository(_context);
            ProductSendPointRepository = new ProductSendPointRepository(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

    }
}
