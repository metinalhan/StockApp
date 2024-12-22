using AutoMapper;
using StockApp.BL.Abstract;
using StockApp.DAL.Repositories;
using StockApp.Entities.Models;

namespace StockApp.BL.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IExcelService _excelService;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> repository, IExcelService excelService, IMapper mapper)
        {
            _repository = repository;
            _excelService = excelService;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync() as List<Product>;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductByBarcodeAsync(string barcode)
        {
            return await _repository.FindAsync(x=> x.Barcode.CompareTo(barcode)==0);
        }

        public async void AddProduct(Product product)
        {
            _repository.Add(product);
            await _repository.SaveAsync();
        }

        public async void UpdateProduct(Product product)
        {
            _repository.Update(product);
            await _repository.SaveAsync();
        }

        public async void DeleteProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product != null)
            {
                product.IsActive = false;
                _repository.Update(product);
                await _repository.SaveAsync();
            }
        }

        public async Task ImportProductsFromExcel(string filePath)
        {
            var newProducts = new List<Product>();

            var productsFromExcel = await _excelService.ReadExcelAsync(filePath);
            var existingProducts = await _repository.GetAllAsync();

            foreach (var excelProduct in productsFromExcel)
            {
                var existingProduct = existingProducts.FirstOrDefault(p => p.Barcode == excelProduct.Barcode);

                if (existingProduct != null)
                {                   
                    existingProduct.Price = excelProduct.Price;
                    existingProduct.UpdatedDate = DateTime.Now;
                    _repository.Update(existingProduct);
                }
                else
                {                   
                    var newProduct = new Product
                    {
                        Barcode = excelProduct.Barcode,
                        Name = excelProduct.Name,
                        Price = excelProduct.Price,  
                        Stock = excelProduct.Stock
                    };
                    _repository.Add(newProduct);
                }
            }

           // var productsMapped = _mapper.Map<List<Product>>(productsFromExcel);
           // _repository.AddRange(productsMapped);
            await _repository.SaveAsync();
        }
    }
}
