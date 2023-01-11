namespace PiecykPolHurt.DataLayer.Repositories
{
    using PiecykPolHurt.Model.Entities;

    public interface IDictionaryValueRepository : IReadOnlyRepository<DictionaryValue> { }
    public class DictionaryValueRepository : BaseRepository<DictionaryValue>, IDictionaryValueRepository
    {
        public DictionaryValueRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
