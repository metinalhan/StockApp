using StockApp.Entities.Models;
using StockApp.UI.ViewModel;
using System.Windows.Controls;

namespace StockApp.UI.Views
{
    /// <summary>
    /// Interaction logic for ProductListView.xaml
    /// </summary>
    public partial class ProductListView : UserControl
    {
        private ProductListVM _viewModel;
        public ProductListView(ProductListVM viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;

            viewModel.LoadProducts();
        }

        private void ExpiryDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datePicker = (DatePicker)sender;
            var selectedProduct = (Product)datePicker.DataContext;

            if (selectedProduct != null)
            {
                selectedProduct.ExpirationDate = datePicker.SelectedDate.Value;
                _viewModel.UpdateProductExpiryDate(selectedProduct);
            }
        }
    }
}
