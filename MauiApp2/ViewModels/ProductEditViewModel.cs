using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Helpers;
using MauiApp2.Repositories.Interfaces;
using MauiApp2.Services.Interfaces;

namespace MauiApp2.ViewModels
{
    [QueryProperty(nameof(ProductId), "ProductId")]
    public partial class ProductEditViewModel : BaseViewModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IStateService _stateService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ProductEditViewModel(
            IProductRepository productRepository,
            IStateService stateService,
            INavigationService navigationService,
            IDialogService dialogService)
        {
            _productRepository = productRepository;
            _stateService = stateService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            Title = "Edit Product";
        }

        [ObservableProperty] private int productId;
        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private string description = string.Empty;
        [ObservableProperty] private string price = string.Empty;
        [ObservableProperty] private string quantity = string.Empty;
        [ObservableProperty] private string category = string.Empty;
        [ObservableProperty] private string imagePath = string.Empty;

        partial void OnProductIdChanged(int value)
        {
            _ = LoadProductAsync(value);
        }

        private async Task LoadProductAsync(int id)
        {
            try
            {
                IsBusy = true;
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null) return;

                Name = product.Name;
                Description = product.Description;
                Price = product.Price.ToString();
                Quantity = product.Quantity.ToString();
                Category = product.Category;
                ImagePath = product.ImagePath;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task PickImageAsync()
        {
            var result = await MediaPicker.Default.PickPhotoAsync();
            if (result != null)
                ImagePath = result.FullPath;
        }

        private bool Validate()
        {
            ClearError();

            if (!Validators.IsValidProductName(Name)) { SetError("Product name is required."); return false; }
            if (!decimal.TryParse(Price, out var priceValue) || !Validators.IsValidPrice(priceValue)) { SetError("Enter a valid price."); return false; }
            if (!int.TryParse(Quantity, out var qty) || !Validators.IsValidQuantity(qty)) { SetError("Enter a valid quantity."); return false; }
            if (!Validators.IsValidCategory(Category)) { SetError("Category is required."); return false; }

            return true;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (!Validate()) return;

            try
            {
                IsBusy = true;
                var product = await _productRepository.GetByIdAsync(ProductId);
                if (product == null) return;

                product.Name = Name;
                product.Description = Description;
                product.Price = decimal.Parse(Price);
                product.Quantity = int.Parse(Quantity);
                product.Category = Category;
                product.ImagePath = ImagePath;

                await _productRepository.UpdateAsync(product);
                var allProducts = await _productRepository.GetAllAsync();
                _stateService.SetProductList(allProducts);

                await _dialogService.ShowToastAsync(Constants.Messages.ProductSaved);
                await _navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                SetError($"{Constants.Messages.GenericError} ({ex.Message})");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
