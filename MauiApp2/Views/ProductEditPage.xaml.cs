using MauiApp2.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp2.Views
{
    public partial class ProductEditPage : ContentPage
    {
        public ProductEditPage()
            : this(App.Services?.GetRequiredService<ProductEditViewModel>() ?? throw new InvalidOperationException("ProductEditViewModel was not registered."))
        {
        }

        public ProductEditPage(ProductEditViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
