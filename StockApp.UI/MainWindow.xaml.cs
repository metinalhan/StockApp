using StockApp.BL.Services;
using StockApp.Entities.Models;
using System.Windows;

namespace StockApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProductService _productService;

        public MainWindow(ProductService productService)
        {
            InitializeComponent();
            _productService = productService;

            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = _productService.GetAllProducts();
            ProductListView.ItemsSource = products;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            var productName = ProductNameTextBox.Text;
            if (!string.IsNullOrEmpty(productName))
            {
                var product = new Product { Name = productName };
                _productService.AddProduct(product);
                LoadProducts();
                ProductNameTextBox.Clear();
            }
        }

        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductListView.SelectedItem as Product;
            if (selectedProduct != null && !string.IsNullOrEmpty(ProductNameTextBox.Text))
            {
                selectedProduct.Name = ProductNameTextBox.Text;
                _productService.UpdateProduct(selectedProduct);
                LoadProducts();
                ProductNameTextBox.Clear();
            }
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductListView.SelectedItem as Product;
            if (selectedProduct != null)
            {
                _productService.DeleteProduct(selectedProduct.Id);
                LoadProducts();
            }
        }
    }
}