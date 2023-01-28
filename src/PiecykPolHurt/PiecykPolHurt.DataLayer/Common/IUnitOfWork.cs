using PiecykPolHurt.DataLayer.Repositories;

namespace PiecykPolHurt.DataLayer.Common
{
    public interface IUnitOfWork
    {
        IDictionaryValueRepository DictionaryValueRepository { get; set; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        ISendPointRepository SendPointRepository { get; }
        IReportDefinitionRepository ReportDefinitionRepository { get; }
        string ConnectionString { get; }
        Task SaveChangesAsync();
    }
}