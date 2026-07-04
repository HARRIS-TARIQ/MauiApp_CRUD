using CommunityToolkit.Maui.Views;
using MauiApp2.Models;

namespace MauiApp2.Views.Popups
{
    public partial class ProductQuickViewPopup : Popup
    {
        public ProductQuickViewPopup(Product product)
        {
            InitializeComponent();
            BindingContext = product;
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            // Close(result) -> ye result caller ko ShowPopupAsync() ke return value mein milta hai
            Close(BindingContext);
        }
    }
}
