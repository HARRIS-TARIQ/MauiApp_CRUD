using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Repositories.Interfaces;
using MauiApp2.Services.Interfaces;
using MauiApp2.Views;

namespace MauiApp2.ViewModels
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authService;
        private readonly INavigationService _navigationService;
        private readonly IProductRepository _productRepository;
        private readonly IStateService _stateService;

        public DashboardViewModel(
            IAuthenticationService authService,
            INavigationService navigationService,
            IProductRepository productRepository,
            IStateService stateService)
        {
            _authService = authService;
            _navigationService = navigationService;
            _productRepository = productRepository;
            _stateService = stateService;
            Title = "Dashboard";

            _stateService.OnUserChanged += () => CurrentUserName = _stateService.CurrentUser?.FullName ?? "Guest";
        }

        [ObservableProperty]
        private string currentUserName = string.Empty;

        [ObservableProperty]
        private int totalProducts;

        [ObservableProperty]
        private decimal totalInventoryValue;

        [RelayCommand]
        private async Task AppearingAsync()
        {
            try
            {
                IsBusy = true;
                var user = await _authService.GetCurrentUserAsync();
                CurrentUserName = user?.FullName ?? "Guest";

                var products = await _productRepository.GetAllAsync();
                TotalProducts = products.Count;
                TotalInventoryValue = products.Sum(p => p.Price * p.Quantity);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToProductsAsync()
        {
            //await _navigationService.NavigateAsync("//DashboardPage/ProductListPage");
            await _navigationService.NavigateAsync(nameof(ProductListPage));
        }

        [RelayCommand]
        private async Task GoToDevicesAsync()
        {
            await _navigationService.NavigateAsync(nameof(DeviceListPage));
        }

        [RelayCommand]
        private async Task GoToMonitoringAsync()
        {
            await _navigationService.NavigateAsync(nameof(LiveMonitoringPage));
        }

        [RelayCommand]
        private async Task GoToReportsAsync()
        {
            await _navigationService.NavigateAsync(nameof(ReportsPage));
        }

        [RelayCommand]
        private async Task GoToSettingsAsync()
        {
            await _navigationService.NavigateAsync(nameof(SettingsPage));
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            await _authService.LogoutAsync();
            await _navigationService.NavigateToLogin();
        }
    }
}
