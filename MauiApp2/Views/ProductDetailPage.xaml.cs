using MauiApp2.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp2.Views
{
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage()
            : this(App.Services?.GetRequiredService<ProductDetailViewModel>() ?? throw new InvalidOperationException("ProductDetailViewModel was not registered."))
        {
        }

        public ProductDetailPage(ProductDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
