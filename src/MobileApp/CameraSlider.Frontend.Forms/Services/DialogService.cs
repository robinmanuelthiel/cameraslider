using System;
using System.Threading.Tasks;
using CameraSlider.Frontend.Shared.Services;
using Xamarin.Forms;

namespace CameraSlider.Frontend.Forms.Services
{
    public class DialogService : IDialogService
    {
        public async Task DisplayDialogAsync(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayDialogAsync(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
