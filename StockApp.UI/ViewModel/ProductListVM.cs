using StockApp.BL.Services;
using StockApp.Entities.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Windows;

namespace StockApp.UI.ViewModel
{
    public class ProductListVM : INotifyPropertyChanged
    {
        private readonly ProductService _productService;

        public ObservableCollection<Product> Products { get; set; }

        public ProductListVM(ProductService productService)
        {

            _productService = productService;
            
            //LoadProducts();
        }      

        public async void LoadProducts()
        {
            Products = new ObservableCollection<Product>();
            var products = await _productService.GetAllProductsAsync();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        public async void UpdateProductExpiryDate(Product product)
        {
            var dbProduct = await _productService.GetProductByIdAsync(product.Id);

            if (dbProduct != null)
            {
                dbProduct.ExpirationDate = product.ExpirationDate;
                dbProduct.UpdatedDate = DateTime.Now;

                try
                {
                    _productService.UpdateProduct(dbProduct);
                    MessageBox.Show("Son kullanma tarihi güncellendi.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (DbUpdateConcurrencyException)
                {
                    MessageBox.Show("Veritabanında bir hata oluştu. Lütfen tekrar deneyin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
