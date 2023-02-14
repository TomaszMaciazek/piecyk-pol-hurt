using PiecykPolHurt.Model.Entities;
using PiecykPolHurt.Model.Enums;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public interface INotificationTemplateRepository : IReadOnlyRepository<NotificationTemplate> {
        IQueryable<NotificationTemplate> GetByType(NotificationType type);
    }
    public class NotificationTemplateRepository : BaseRepository<NotificationTemplate>, INotificationTemplateRepository
    {
        public NotificationTemplateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<NotificationTemplate> GetByType(NotificationType type) => _context.NotificationTemplates.Where(x => x.Type == type);
    }
}
