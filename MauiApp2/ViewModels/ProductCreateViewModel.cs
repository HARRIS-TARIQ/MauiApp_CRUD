using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Helpers;
using MauiApp2.Models;
using MauiApp2.Repositories.Interfaces;
using MauiApp2.Services.Interfaces;

namespace MauiApp2.ViewModels
{
    public partial class ProductCreateViewModel : BaseViewModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IStateService _stateService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ProductCreateViewModel(
            IProductRepository productRepository,
            IStateService stateService,
            INavigationService navigationService,
            IDialogService dialogService)
        {
            _productRepository = productRepository;
            _stateService = stateService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            Title = "Add Product";
        }

        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private string description = string.Empty;
        [ObservableProperty] private string price = string.Empty;
        [ObservableProperty] private string quantity = string.Empty;
        [ObservableProperty] private string category = string.Empty;
        [ObservableProperty] private string imagePath = string.Empty;

        [RelayCommand]
        private async Task PickImageAsync()
        {
            try
            {
                var result = await MediaPicker.Default.PickPhotoAsync();
                if (result != null)
                {
                    ImagePath = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                SetError($"Could not pick image: {ex.Message}");
            }
        }

        private bool Validate()
        {
            ClearError();

            if (!Validators.IsValidProductName(Name))
            {
                SetError("Product name is required.");
                return false;
            }

            if (!decimal.TryParse(Price, out var priceValue) || !Validators.IsValidPrice(priceValue))
            {
                SetError("Enter a valid price greater than 0.");
                return false;
            }

            if (!int.TryParse(Quantity, out var qtyValue) || !Validators.IsValidQuantity(qtyValue))
            {
                SetError("Enter a valid quantity (0 or more).");
                return false;
            }

            if (!Validators.IsValidCategory(Category))
            {
                SetError("Category is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                SetError("Description is required.");
                return false;
            }

            return true;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (!Validate()) return;

            try
            {
                IsBusy = true;

                var product = new Product
                {
                    Name = Name,
                    Description = Description,
                    Price = decimal.Parse(Price),
                    Quantity = int.Parse(Quantity),
                    Category = Category,
                    ImagePath = ImagePath
                };

                await _productRepository.AddAsync(product);
                var allProducts = await _productRepository.GetAllAsync();
                _stateService.SetProductList(allProducts);

                //await _dialogService.ShowToastAsync(Constants.Messages.ProductSaved);
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
