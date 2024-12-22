using StockApp.DAL.Context;
using StockApp.Entities;
using System.Data.Entity;
using System.Linq.Expressions;

namespace StockApp.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.Where(w=>w.IsActive).ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

        public void Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            entity.UpdatedDate = DateTime.Now;          
        }

        public void Delete(T entity)
        {
            entity.IsActive = false;
            Update(entity);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public void AddRange(IEnumerable<T> entity)
        {
           _dbSet.AddRange(entity);
        }
    }
}
