using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MauiApp2.Services.Interfaces;

namespace MauiApp2.Services.Implementation
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string title, string message, string cancel = "OK")
        {
            return Application.Current!.MainPage!.DisplayAlert(title, message, cancel);
        }

        public Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No")
        {
            return Application.Current!.MainPage!.DisplayAlert(title, message, accept, cancel);
        }

        public async Task ShowToastAsync(string message)
        {
            var toast = Toast.Make(message, ToastDuration.Short);
            await toast.Show();
        }
    }
}
