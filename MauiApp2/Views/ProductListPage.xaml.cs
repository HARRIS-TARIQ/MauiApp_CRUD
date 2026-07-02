using MauiApp2.ViewModels;

namespace MauiApp2.Views
{
    public partial class ProductListPage : ContentPage
    {
        public ProductListPage(ProductListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
