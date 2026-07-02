using System.Collections.ObjectModel;
using MauiApp2.Models;
using MauiApp2.Services.Interfaces;

namespace MauiApp2.Services.Implementation
{
    // Centralized State Management Service.
    // No page communicates directly with another page - everything flows through here.
    public class StateService : IStateService
    {
        public event Action? OnUserChanged;
        public event Action? OnProductListChanged;
        public event Action? OnThemeChanged;

        private User? _currentUser;
        public User? CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnUserChanged?.Invoke();
            }
        }

        public Product? SelectedProduct { get; set; }

        public bool IsAuthenticated { get; set; }

        private string _appTheme = "Light";
        public string AppTheme
        {
            get => _appTheme;
            set
            {
                _appTheme = value;
                OnThemeChanged?.Invoke();
            }
        }

        public ObservableCollection<Product> ProductList { get; set; } = new();

        public void SetCurrentUser(User? user)
        {
            CurrentUser = user;
            IsAuthenticated = user != null;
        }

        public void SetProductList(List<Product> products)
        {
            ProductList.Clear();
            foreach (var p in products)
                ProductList.Add(p);

            NotifyProductListChanged();
        }

        public void NotifyProductListChanged()
        {
            OnProductListChanged?.Invoke();
        }

        public void ClearState()
        {
            CurrentUser = null;
            IsAuthenticated = false;
            SelectedProduct = null;
            ProductList.Clear();
        }
    }
}
