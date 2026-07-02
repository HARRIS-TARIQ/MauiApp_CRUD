using MauiApp2.Models;

namespace MauiApp2.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
        Task<bool> IsUserLoggedInAsync();
        Task<User?> GetCurrentUserAsync();
        Task TryAutoLoginAsync();
    }
}
