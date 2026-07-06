//using MauiApp2.Views;

//namespace MauiApp2
//{
//    public partial class AppShell : Shell
//    {
//        public AppShell()
//        {
//            InitializeComponent();

//            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
//            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
//            Routing.RegisterRoute(nameof(ProductListPage), typeof(ProductListPage));
//            Routing.RegisterRoute(nameof(ProductCreatePage), typeof(ProductCreatePage));
//            Routing.RegisterRoute(nameof(ProductEditPage), typeof(ProductEditPage));
//            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
//            Routing.RegisterRoute(nameof(DeviceListPage), typeof(DeviceListPage));
//            Routing.RegisterRoute(nameof(DeviceDetailsPage), typeof(DeviceDetailsPage));
//            Routing.RegisterRoute(nameof(LiveMonitoringPage), typeof(LiveMonitoringPage));
//            Routing.RegisterRoute(nameof(DeviceConfigurationPage), typeof(DeviceConfigurationPage));
//            Routing.RegisterRoute(nameof(DeviceDiagnosticsPage), typeof(DeviceDiagnosticsPage));
//            Routing.RegisterRoute(nameof(ReportsPage), typeof(ReportsPage));
//            Routing.RegisterRoute(nameof(ReportDetailsPage), typeof(ReportDetailsPage));
//            Routing.RegisterRoute(nameof(DataExportPage), typeof(DataExportPage));
//            Routing.RegisterRoute(nameof(UserManagementPage), typeof(UserManagementPage));
//            Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));
//            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
//            Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
//            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
//            Routing.RegisterRoute(nameof(HelpPage), typeof(HelpPage));
//        }
//    }
//}

using MauiApp2.Views;

namespace MauiApp2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
            Routing.RegisterRoute(nameof(ProductListPage), typeof(ProductListPage));

            // Register only pages that are NOT declared in AppShell.xaml

            Routing.RegisterRoute(nameof(ProductCreatePage), typeof(ProductCreatePage));
            Routing.RegisterRoute(nameof(ProductEditPage), typeof(ProductEditPage));
            Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));

            Routing.RegisterRoute(nameof(DeviceDetailsPage), typeof(DeviceDetailsPage));
            Routing.RegisterRoute(nameof(DeviceConfigurationPage), typeof(DeviceConfigurationPage));
            Routing.RegisterRoute(nameof(DeviceDiagnosticsPage), typeof(DeviceDiagnosticsPage));

            Routing.RegisterRoute(nameof(ReportDetailsPage), typeof(ReportDetailsPage));
            Routing.RegisterRoute(nameof(DataExportPage), typeof(DataExportPage));

            Routing.RegisterRoute(nameof(UserManagementPage), typeof(UserManagementPage));
            Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));

            Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(HelpPage), typeof(HelpPage));
        }
    }
}
