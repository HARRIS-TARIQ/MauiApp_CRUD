using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Helpers;
using MauiApp2.Models;
using MauiApp2.Repositories.Interfaces;
using MauiApp2.Services.Interfaces;

namespace MauiApp2.ViewModels
{
    public partial class ProductListViewModel : BaseViewModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IStateService _stateService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ProductListViewModel(
            IProductRepository productRepository,
            IStateService stateService,
            INavigationService navigationService,
            IDialogService dialogService)
        {
            _productRepository = productRepository;
            _stateService = stateService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            Title = "Products";

            _stateService.OnProductListChanged += () => Products = new ObservableCollection<Product>(_stateService.ProductList);
        }

        [ObservableProperty]
        private ObservableCollection<Product> products = new();

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool isEmpty;

        [RelayCommand]
        private async Task AppearingAsync()
        {
            await LoadProductsAsync();
        }

        [RelayCommand]
        private async Task LoadProductsAsync()
        {
            try
            {
                IsBusy = true;
                var list = await _productRepository.GetAllAsync();
                _stateService.SetProductList(list);
                IsEmpty = list.Count == 0;
            }
            catch (Exception ex)
            {
                SetError($"{Constants.Messages.GenericError} ({ex.Message})");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            try
            {
                IsBusy = true;
                var results = await _productRepository.SearchAsync(SearchText);
                Products = new ObservableCollection<Product>(results);
                IsEmpty = results.Count == 0;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void SortByPrice()
        {
            Products = new ObservableCollection<Product>(Products.OrderBy(p => p.Price));
        }

        [RelayCommand]
        private void SortByName()
        {
            Products = new ObservableCollection<Product>(Products.OrderBy(p => p.Name));
        }

        [RelayCommand]
        private async Task GoToCreateAsync()
        {
            await _navigationService.NavigateAsync(Constants.Routes.ProductCreate);
        }

        [RelayCommand]
        private async Task ViewDetailsAsync(Product product)
        {
            _stateService.SelectedProduct = product;
            await _navigationService.NavigateToProductDetails(product.Id);
        }

        [RelayCommand]
        private async Task DeleteAsync(Product product)
        {
            var confirm = await _dialogService.ShowConfirmationAsync(
                "Delete Product", $"Are you sure you want to delete '{product.Name}'?");

            if (!confirm) return;

            var success = await _productRepository.DeleteAsync(product.Id);
            if (success)
            {
                await _dialogService.ShowToastAsync(Constants.Messages.ProductDeleted);
                await LoadProductsAsync();
            }
        }
    }
}
