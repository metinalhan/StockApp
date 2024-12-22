using StockApp.Entities;
using System.Linq.Expressions;

namespace StockApp.DAL.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddRange(IEnumerable<T> entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
