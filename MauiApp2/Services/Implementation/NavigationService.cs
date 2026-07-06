using MauiApp2.Services.Interfaces;
using MauiApp2.Views;

namespace MauiApp2.Services.Implementation;


public class NavigationService : INavigationService
{
    private bool _isNavigating;

    private async Task Navigate(Func<Task> navigation)
    {
        if (_isNavigating)
            return;

        try
        {
            _isNavigating = true;
            await navigation();
        }
        finally
        {
            _isNavigating = false;
        }
    }

    public Task NavigateAsync(string route)
        => Navigate(() => Shell.Current.GoToAsync(route));

    public Task NavigateAsync(string route, IDictionary<string, object> parameters)
        => Navigate(() => Shell.Current.GoToAsync(route, parameters));

    public Task GoBackAsync()
        => Navigate(() => Shell.Current.GoToAsync(".."));

    public Task NavigateToLogin()
        => Navigate(() => Shell.Current.GoToAsync("//LoginPage"));

    public Task NavigateToDashboard()
        => Navigate(() => Shell.Current.GoToAsync("//DashboardPage"));

    public Task NavigateToProductDetails(int productId)
        => Navigate(() =>
            Shell.Current.GoToAsync(
                nameof(ProductDetailPage),
                new Dictionary<string, object>
                {
                    { "ProductId", productId }
                }));
}

//public class NavigationService : INavigationService
//{
//    public Task NavigateAsync(string route)
//    {
//        return Shell.Current.GoToAsync(route);
//    }

//    public Task NavigateAsync(string route, IDictionary<string, object> parameters)
//    {
//        return Shell.Current.GoToAsync(route, parameters);
//    }

//    public Task GoBackAsync()
//    {
//        return Shell.Current.GoToAsync("..");
//    }

//    public Task NavigateToLogin()
//    {
//        return Shell.Current.GoToAsync("//LoginPage");
//    }

//    public Task NavigateToDashboard()
//    {
//        return Shell.Current.GoToAsync("//DashboardPage");
//    }

//    public Task NavigateToProductDetails(int productId)
//    {
//        return Shell.Current.GoToAsync(
//            nameof(ProductDetailPage),
//            new Dictionary<string, object>
//            {
//                { "ProductId", productId }
//            });
//    }
//}