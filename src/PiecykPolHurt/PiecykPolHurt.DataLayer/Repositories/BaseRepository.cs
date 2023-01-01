using PiecykPolHurt.Model.Entities;

namespace PiecykPolHurt.DataLayer.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity, new()
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll() => _context.Set<T>();

        public IQueryable<T> GetById(int id) => _context.Set<T>().Where(x => x.Id == id);

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(int id)
        {
            T entity = new() { Id = id };
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
