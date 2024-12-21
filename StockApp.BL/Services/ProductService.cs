using StockApp.DAL.Repositories;
using StockApp.Entities.Models;

namespace StockApp.BL.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public List<Product> GetAllProducts()
        {
            return _repository.GetAll() as List<Product>;
        }

        public Product GetProductById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddProduct(Product product)
        {
            _repository.Add(product);
            _repository.Save();
        }

        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
            _repository.Save();
        }

        public void DeleteProduct(int id)
        {
            var product = _repository.GetById(id);
            if (product != null)
            {
                product.IsActive = false;
                _repository.Update(product);
                _repository.Save();
            }
        }
    }
}
