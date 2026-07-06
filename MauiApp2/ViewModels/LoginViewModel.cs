using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Helpers;
using MauiApp2.Services.Interfaces;

namespace MauiApp2.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public LoginViewModel(
            IAuthenticationService authService,
            INavigationService navigationService,
            IDialogService dialogService)
        {
            _authService = authService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            Title = "Login";
        }

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private bool rememberMe;

        [ObservableProperty]
        private bool isPasswordVisible;

        [RelayCommand]
        private void TogglePasswordVisibility()
        {
            IsPasswordVisible = !IsPasswordVisible;
        }

        [RelayCommand]
        private void ForgotPassword()
        {
            SetError("Password recovery is not configured in this demo build.");
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            ClearError();

            if (!Validators.IsValidEmail(Email))
            {
                SetError("Please enter a valid email.");
                return;
            }

            if (!Validators.IsValidPassword(Password))
            {
                SetError("Password is required.");
                return;
            }

            try
            {
                IsBusy = true;
                var success = await _authService.LoginAsync(Email, Password, RememberMe);

                if (!success)
                {
                    SetError(Constants.Messages.LoginFailed);
                    return;
                }

                await _navigationService.NavigateToDashboard();
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
        private async Task AppearingAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (await _authService.IsUserLoggedInAsync())
                {
                    await _navigationService.NavigateToDashboard();
                }
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
