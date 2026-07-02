using CommunityToolkit.Maui;
using MauiApp2.Data;
using MauiApp2.Repositories.Implementation;
using MauiApp2.Repositories.Interfaces;
using MauiApp2.Services.Implementation;
using MauiApp2.Services.Interfaces;
using MauiApp2.ViewModels;
using MauiApp2.Views;
using Microsoft.Extensions.Logging;

namespace MauiApp2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // ---------- Database ----------
            builder.Services.AddSingleton<AppDbContext>();
            builder.Services.AddSingleton<DatabaseInitializer>();

            // ---------- Repositories ----------
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();

            // ---------- Services ----------
            builder.Services.AddSingleton<IStateService, StateService>();
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();

            // ---------- ViewModels ----------
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<ProductListViewModel>();
            builder.Services.AddTransient<ProductCreateViewModel>();
            builder.Services.AddTransient<ProductEditViewModel>();
            builder.Services.AddTransient<ProductDetailViewModel>();

            // ---------- Views (Pages) ----------
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<ProductListPage>();
            builder.Services.AddTransient<ProductCreatePage>();
            builder.Services.AddTransient<ProductEditPage>();
            builder.Services.AddTransient<ProductDetailPage>();


            //return builder.Build();
            var app = builder.Build();

            // Initialize database and seed data at startup
            using (var scope = app.Services.CreateScope())
            {
                var initializer = app.Services.GetRequiredService<DatabaseInitializer>();
                initializer.InitializeAsync().GetAwaiter().GetResult();
            }

            return app;
        }
    }
}
