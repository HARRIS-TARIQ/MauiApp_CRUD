namespace MauiApp2.Services.Interfaces
{
    public interface INavigationService
    {
        Task NavigateAsync(string route);
        Task NavigateAsync(string route, IDictionary<string, object> parameters);
        Task GoBackAsync();
        Task NavigateToDashboard();
        Task NavigateToLogin();
        Task NavigateToProductDetails(int productId);
    }
}
