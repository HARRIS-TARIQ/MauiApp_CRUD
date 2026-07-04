using MauiApp2.Views;

namespace MauiApp2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(ProductListPage), typeof(ProductListPage));
            Routing.RegisterRoute(nameof(ProductCreatePage), typeof(ProductCreatePage));
            Routing.RegisterRoute(nameof(ProductEditPage), typeof(ProductEditPage));
            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
            Routing.RegisterRoute(nameof(UiShowcasePage), typeof(UiShowcasePage));
        }
    }
}
