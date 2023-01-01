using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public interface IDictionaryValueRepository : IReadOnlyRepository<DictionaryValue> { }
    public class DictionaryValueRepository : BaseRepository<DictionaryValue>, IDictionaryValueRepository
    {
        public DictionaryValueRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
