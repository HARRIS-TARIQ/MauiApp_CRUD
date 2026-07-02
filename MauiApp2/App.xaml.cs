using MauiApp2.Views;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp2
{
    public partial class App : Application
    {
        //private readonly IServiceProvider _serviceProvider;

        public App()
        {
            InitializeComponent();

        }
        //public App(IServiceProvider serviceProvider)
        //{
        //    InitializeComponent();
        //    _serviceProvider = serviceProvider;
        //}

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}