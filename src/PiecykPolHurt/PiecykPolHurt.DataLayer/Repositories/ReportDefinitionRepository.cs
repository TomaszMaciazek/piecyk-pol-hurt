using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public interface IReportDefinitionRepository : IRepository<ReportDefinition> { }

    public class ReportDefinitionRepository : BaseRepository<ReportDefinition>, IReportDefinitionRepository
    {
        public ReportDefinitionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
