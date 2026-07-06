using MauiApp2.Views;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp2
{
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Services = Handler?.MauiContext?.Services;
            return new Window(new AppShell());
        }
    }
}