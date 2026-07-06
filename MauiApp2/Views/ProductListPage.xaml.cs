using MauiApp2.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp2.Views
{
    public partial class ProductListPage : ContentPage
    {
        public ProductListPage()
            : this(App.Services?.GetRequiredService<ProductListViewModel>() ?? throw new InvalidOperationException("ProductListViewModel was not registered."))
        {
        }

        public ProductListPage(ProductListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
