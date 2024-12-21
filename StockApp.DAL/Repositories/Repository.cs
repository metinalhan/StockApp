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

        public IEnumerable<T> GetAll() => _dbSet.Where(w=>w.IsActive).ToList();

        public T GetById(int id) => _dbSet.Find(id);

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).ToList();

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

        public void Save() => _context.SaveChanges();
    }
}
