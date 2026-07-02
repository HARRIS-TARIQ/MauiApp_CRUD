using MauiApp2.ViewModels;

namespace MauiApp2.Views
{
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage(ProductDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
