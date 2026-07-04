using CommunityToolkit.Maui.Extensions;
using MauiApp2.Models;
using MauiApp2.ViewModels;
using MauiApp2.Views.Popups;

namespace MauiApp2.Views
{
    public partial class UiShowcasePage : ContentPage
    {
        private readonly UiShowcaseViewModel _viewModel;

        public UiShowcasePage(UiShowcaseViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        // Note: Popup ko dikhane ke liye Page chahiye hoti hai — ShowPopupAsync() ek Page extension
        // method hai (CommunityToolkit.Maui.Extensions), isi liye ye logic ViewModel mein nahi,
        // code-behind mein hai. ViewModel sirf data/state rakhta hai, UI-hosting yahan hoti hai.
        private async void OnOpenQuickViewClicked(object sender, EventArgs e)
        {
            if (_viewModel.SelectedProduct is not Product product)
                return;

            var popup = new ProductQuickViewPopup(product);

            // ShowPopupAsync popup band hone tak wait karta hai aur uska result wapas deta hai
            var result = await this.ShowPopupAsync(popup);

            _viewModel.LastPopupResult = result.WasDismissedByTappingOutsideOfPopup
                ? "Popup ko bahar tap karke band kiya gaya."
                : $"Popup close hua, result: {(result.Result as Product)?.Name ?? "N/A"}";
        }
    }
}
