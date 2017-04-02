using System;
using System.Threading.Tasks;

namespace CameraSlider.Frontend.Shared.Services
{
    public interface IDialogService
    {
        Task DisplayDialogAsync(string title, string message, string cancel);
        Task<bool> DisplayDialogAsync(string title, string message, string accept, string cancel);
    }
}
