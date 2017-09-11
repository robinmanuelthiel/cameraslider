using CameraSlider.Frontend.Forms.iOS.Services;
using CameraSlider.Frontend.Forms.Services;
using ObjCRuntime;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(EnvironmentCheckServiceiOS))]
namespace CameraSlider.Frontend.Forms.iOS.Services
{
    public class EnvironmentCheckServiceiOS : IEnvironmentCheckService
    {
        public bool IsRunningInRealWorld()
        {
#if DEBUG
            return false;
#endif

            if (Runtime.Arch == Arch.SIMULATOR || Environment.GetEnvironmentVariable("XAMARIN_TEST_CLOUD") != null)
            {
                return false;
            }

            return true;
        }
    }
}
