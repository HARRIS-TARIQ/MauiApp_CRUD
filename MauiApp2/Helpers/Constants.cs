namespace MauiApp2.Helpers
{
    public static class Constants
    {
        public const string DatabaseFilename = "mauicrudapp.db3";

        public static class Routes
        {
            public const string Login = "LoginPage";
            public const string Dashboard = "DashboardPage";
            public const string ProductList = "ProductListPage";
            public const string ProductCreate = "ProductCreatePage";
            public const string ProductEdit = "ProductEditPage";
            public const string ProductDetail = "ProductDetailPage";
        }

        public static class Messages
        {
            public const string LoginFailed = "Invalid email or password.";
            public const string ProductSaved = "Product saved successfully.";
            public const string ProductDeleted = "Product deleted successfully.";
            public const string GenericError = "Something went wrong. Please try again.";
        }
    }
}
