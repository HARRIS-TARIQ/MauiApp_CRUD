using MauiApp2.ViewModels;

namespace MauiApp2.Views
{
    public partial class ProductCreatePage : ContentPage
    {
        public ProductCreatePage(ProductCreateViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
