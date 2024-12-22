using StockApp.BL.Services;
using StockApp.Entities.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockApp.UI.Views
{
    /// <summary>
    /// Interaction logic for ItemScanView.xaml
    /// </summary>
    public partial class ItemScanView : UserControl
    {
        private readonly ProductService _productService;
        public ItemScanView(ProductService productService)
        {
            InitializeComponent();

            tbBarkod.Focus();
            _productService = productService;
        }

        private async void tbBarkod_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            

            try
            {
                if (e.Key == Key.Enter)
                {
                    tblcAranan.Text = tbBarkod.Text;
                    var product = await _productService.GetProductByBarcodeAsync(tbBarkod.Text);

                    
                    lvProduct.ItemsSource = product;

                    tbBarkod.Clear();
                }
                
            }
            catch (Exception)
            {
                tblcSKU.Text = "ERROR !!";
            }
        }

        private void ExpiryDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var datePicker = (DatePicker)sender;
                var selectedProduct = (Product)datePicker.DataContext;

                if (selectedProduct != null)
                {
                    selectedProduct.ExpirationDate = datePicker.SelectedDate.Value;

                    _productService.UpdateProduct(selectedProduct);

                    MessageBox.Show("The Product Expiry Date has been updated", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    tbBarkod.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
