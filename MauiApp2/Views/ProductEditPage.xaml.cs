using MauiApp2.ViewModels;

namespace MauiApp2.Views
{
    public partial class ProductEditPage : ContentPage
    {
        public ProductEditPage(ProductEditViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
