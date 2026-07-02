using MauiApp2.Models;
using MauiApp2.Repositories.Interfaces;
using MauiApp2.Services.Interfaces;

namespace MauiApp2.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStateService _stateService;

        private const string SessionEmailKey = "session_email";
        private const string RememberMeKey = "remember_me";

        public AuthenticationService(IUserRepository userRepository, IStateService stateService)
        {
            _userRepository = userRepository;
            _stateService = stateService;
        }

        public async Task<bool> LoginAsync(string email, string password, bool rememberMe)
        {
            var user = await _userRepository.ValidateUserAsync(email, password);
            if (user == null)
                return false;

            _stateService.SetCurrentUser(user);

            if (rememberMe)
            {
                await SecureStorage.SetAsync(SessionEmailKey, email);
                Preferences.Set(RememberMeKey, true);
            }
            else
            {
                SecureStorage.Remove(SessionEmailKey);
                Preferences.Set(RememberMeKey, false);
            }

            return true;
        }

        public Task LogoutAsync()
        {
            _stateService.ClearState();
            SecureStorage.Remove(SessionEmailKey);
            Preferences.Set(RememberMeKey, false);
            return Task.CompletedTask;
        }

        public async Task<bool> IsUserLoggedInAsync()
        {
            //return Task.FromResult(_stateService.IsAuthenticated);
            if (_stateService.IsAuthenticated)
                return true;

            await TryAutoLoginAsync();

            return _stateService.IsAuthenticated;
        }

        public Task<User?> GetCurrentUserAsync()
        {
            return Task.FromResult(_stateService.CurrentUser);
        }

        public async Task TryAutoLoginAsync()
        {
            var remember = Preferences.Get(RememberMeKey, false);

            System.Diagnostics.Debug.WriteLine($"Remember = {remember}");

            if (!remember)
                return;

            var email = await SecureStorage.GetAsync(SessionEmailKey);

            System.Diagnostics.Debug.WriteLine($"Email = {email}");

            if (string.IsNullOrWhiteSpace(email))
                return;

            var user = await _userRepository.GetByEmailAsync(email);

            System.Diagnostics.Debug.WriteLine(user == null ? "User Not Found" : $"User Found : {user.FullName}");

            if (user != null)
            {
                _stateService.SetCurrentUser(user);
            }
        }
    }
}
