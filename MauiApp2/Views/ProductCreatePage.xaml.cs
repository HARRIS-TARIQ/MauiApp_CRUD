using MauiApp2.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp2.Views
{
    public partial class ProductCreatePage : ContentPage
    {
        public ProductCreatePage()
            : this(App.Services?.GetRequiredService<ProductCreateViewModel>() ?? throw new InvalidOperationException("ProductCreateViewModel was not registered."))
        {
        }

        public ProductCreatePage(ProductCreateViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
