using MauiApp2.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp2.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
            : this(App.Services?.GetRequiredService<LoginViewModel>() ?? throw new InvalidOperationException("LoginViewModel was not registered."))
        {
        }

        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
