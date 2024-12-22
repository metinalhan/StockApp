using Microsoft.Win32;
using StockApp.BL.Abstract;
using StockApp.BL.Services;
using StockApp.Entities.Models;
using StockApp.UI.ViewModel;
using StockApp.UI.Views;
using System.Windows;

namespace StockApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProductService _productService;
        private readonly ProductListVM _viewModel;


        public MainWindow(ProductService productService, ProductListVM viewModel)
        {
            InitializeComponent();
            _productService = productService;
            _viewModel = viewModel;

            frameControl.NavigationService.Navigate(new ItemScanView(_productService));
            // LoadProducts();
        }

        //private void LoadProducts()
        //{
        //    var products = _productService.GetAllProducts();
        //    ProductListView.ItemsSource = products;
        //}

        private void ProductList_Click(object sender, RoutedEventArgs e)
        {           
            frameControl.NavigationService.Navigate(new ProductListView(_viewModel));
            // var products = await _productService.GetAllProductsAsync();
            //   ProductListView.ItemsSource = products;
        }

        private async void ImportFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Bir Excel Dosyası Seçin"
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = openFileDialog.FileName;

                lblLoading.Visibility = Visibility.Visible;


                try
                {
                    await _productService.ImportProductsFromExcel(filePath);
                    MessageBox.Show("The Products have been imported successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally { lblLoading.Visibility = Visibility.Hidden; }
                  

              //  MessageBox.Show($"Toplam {data.Count} satır okundu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ItemScan_Click(object sender, RoutedEventArgs e)
        {
            frameControl.NavigationService.Navigate(new ItemScanView(_productService));
        }


        //private void AddProductButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var productName = ProductNameTextBox.Text;
        //    if (!string.IsNullOrEmpty(productName))
        //    {
        //        var product = new Product { Name = productName };
        //        _productService.AddProduct(product);
        //        LoadProducts();
        //        ProductNameTextBox.Clear();
        //    }
        //}

        //private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedProduct = ProductListView.SelectedItem as Product;
        //    if (selectedProduct != null && !string.IsNullOrEmpty(ProductNameTextBox.Text))
        //    {
        //        selectedProduct.Name = ProductNameTextBox.Text;
        //        _productService.UpdateProduct(selectedProduct);
        //        LoadProducts();
        //        ProductNameTextBox.Clear();
        //    }
        //}

        //private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedProduct = ProductListView.SelectedItem as Product;
        //    if (selectedProduct != null)
        //    {
        //        _productService.DeleteProduct(selectedProduct.Id);
        //        LoadProducts();
        //    }
        //}
    }
}