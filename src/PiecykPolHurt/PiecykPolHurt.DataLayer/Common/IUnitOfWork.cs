using Microsoft.EntityFrameworkCore.Storage;
using PiecykPolHurt.DataLayer.Repositories;

namespace PiecykPolHurt.DataLayer.Common
{
    public interface IUnitOfWork
    {
        IDictionaryValueRepository DictionaryValueRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        ISendPointRepository SendPointRepository { get; }
        IReportDefinitionRepository ReportDefinitionRepository { get; }
        IProductSendPointRepository ProductSendPointRepository { get; }
        INotificationTemplateRepository NotificationTypeRepository { get; }
        IUserRepository UserRepository { get; }
        string ConnectionString { get; }
        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}