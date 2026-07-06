using MauiApp2.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp2.Views
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
            : this(App.Services?.GetRequiredService<DashboardViewModel>() ?? throw new InvalidOperationException("DashboardViewModel was not registered."))
        {
        }

        public DashboardPage(DashboardViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
