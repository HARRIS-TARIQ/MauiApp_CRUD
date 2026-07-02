using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Helpers;
using MauiApp2.Models;
using MauiApp2.Repositories.Interfaces;
using MauiApp2.Services.Interfaces;

namespace MauiApp2.ViewModels
{
    [QueryProperty(nameof(ProductId), "ProductId")]
    public partial class ProductDetailViewModel : BaseViewModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IStateService _stateService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ProductDetailViewModel(
            IProductRepository productRepository,
            IStateService stateService,
            INavigationService navigationService,
            IDialogService dialogService)
        {
            _productRepository = productRepository;
            _stateService = stateService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            Title = "Product Details";
        }

        [ObservableProperty] private int productId;
        [ObservableProperty] private Product? product;

        partial void OnProductIdChanged(int value)
        {
            _ = LoadAsync(value);
        }

        private async Task LoadAsync(int id)
        {
            try
            {
                IsBusy = true;
                Product = await _productRepository.GetByIdAsync(id);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task EditAsync()
        {
            if (Product == null) return;

            var parameters = new Dictionary<string, object> { { "ProductId", Product.Id } };
            await _navigationService.NavigateAsync(Constants.Routes.ProductEdit, parameters);
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (Product == null) return;

            var confirm = await _dialogService.ShowConfirmationAsync("Delete Product", $"Delete '{Product.Name}'?");
            if (!confirm) return;

            await _productRepository.DeleteAsync(Product.Id);
            var allProducts = await _productRepository.GetAllAsync();
            _stateService.SetProductList(allProducts);

            await _dialogService.ShowToastAsync(Constants.Messages.ProductDeleted);
            await _navigationService.GoBackAsync();
        }
    }
}
