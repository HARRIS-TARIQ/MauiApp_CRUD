using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Models;
using MauiApp2.Repositories.Interfaces;

namespace MauiApp2.ViewModels
{
    // Is ViewModel ka maqsad sirf ye dikhana hai ke CommunityToolkit.Maui + Maui.DataGrid
    // ke rich components ko real Product data ke sath kesy bind karte hain.
    public partial class UiShowcaseViewModel : BaseViewModel
    {
        private readonly IProductRepository _productRepository;

        public UiShowcaseViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            Title = "UI Components Playground";
        }

        [ObservableProperty]
        private ObservableCollection<Product> products = new();

        [ObservableProperty]
        private Product? selectedProduct;

        // Popup se wapas aane wala result yahan store hota hai, sirf demo ke liye
        [ObservableProperty]
        private string lastPopupResult = "Koi popup abhi tak close nahi hua.";

        [RelayCommand]
        private async Task AppearingAsync()
        {
            try
            {
                IsBusy = true;
                var list = await _productRepository.GetAllAsync();
                Products = new ObservableCollection<Product>(list);
            }
            finally
            {
                IsBusy = false;
            }
        }

        // ---------- Toast: chhota, auto-dismiss message (CommunityToolkit.Maui.Alerts) ----------
        [RelayCommand]
        private async Task ShowToastAsync()
        {
            var toast = Toast.Make("Ye ek Toast hai — 2-3 second mein khud gayab ho jayega.", ToastDuration.Short, 14);
            await toast.Show();
        }

        // ---------- Snackbar: action button ke sath message, jesy "Undo" ----------
        [RelayCommand]
        private async Task ShowSnackbarAsync()
        {
            var options = new SnackbarOptions
            {
                BackgroundColor = Color.FromArgb("#2A2A2A"),
                TextColor = Colors.White,
                ActionButtonTextColor = Color.FromArgb("#8FE3B0"),
                CornerRadius = new CornerRadius(14),
                Font = Microsoft.Maui.Font.SystemFontOfSize(14)
            };

            var snackbar = Snackbar.Make(
                "Product list refresh ho gayi.",
                action: async () => await AppearingAsync(),
                actionButtonText: "Refresh Again",
                duration: TimeSpan.FromSeconds(4),
                visualOptions: options);

            await snackbar.Show();
        }

        // ---------- Popup: CommunityToolkit.Maui.Views.Popup, page ke code-behind se open hota hai ----------
        // Isi liye actual ShowPopupAsync() call UiShowcasePage.xaml.cs mein hai (Popup ko Page chahiye hoti hai).
        [RelayCommand]
        private void SelectProduct(Product product)
        {
            SelectedProduct = product;
        }
    }
}
