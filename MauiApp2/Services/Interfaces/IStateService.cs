using System.Collections.ObjectModel;
using MauiApp2.Models;

namespace MauiApp2.Services.Interfaces
{
    public interface IStateService
    {
        User? CurrentUser { get; set; }
        Product? SelectedProduct { get; set; }
        bool IsAuthenticated { get; set; }
        string AppTheme { get; set; }
        ObservableCollection<Product> ProductList { get; set; }

        event Action? OnUserChanged;
        event Action? OnProductListChanged;
        event Action? OnThemeChanged;

        void SetCurrentUser(User? user);
        void SetProductList(List<Product> products);
        void NotifyProductListChanged();
        void ClearState();
    }
}
