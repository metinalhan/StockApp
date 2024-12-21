using StockApp.DAL.Context;
using StockApp.Entities.Models;

namespace StockApp.DAL.Repositories
{
    public class UnitOfWork: IDisposable
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        private Repository<Product> _productRepository;

        public IRepository<Product> ProductRepository => _productRepository ??= new Repository<Product>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
