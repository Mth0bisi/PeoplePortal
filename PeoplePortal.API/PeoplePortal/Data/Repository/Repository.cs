using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetByCode(int code);
        Task Create(T entity);
        Task CreateRange(List<T> entity);
        void Update(T entity);
        Task Save();
        void Delete(T entity);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(DataContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public async Task<List<T>> GetAll() =>
            await _entities.ToListAsync();

        public async Task<T> GetByCode(int code) =>
             await _entities.FindAsync(code);

        public async Task Create(T entity) =>
         await _context.AddAsync(entity);

        public async Task CreateRange(List<T> entity)
        {
            await _context.AddRangeAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task Save() =>
      await _context.SaveChangesAsync();
    }
}
