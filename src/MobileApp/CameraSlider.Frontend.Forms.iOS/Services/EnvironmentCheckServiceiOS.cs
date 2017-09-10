using CameraSlider.Frontend.Forms.iOS.Services;
using CameraSlider.Frontend.Forms.Services;
using ObjCRuntime;

[assembly: Xamarin.Forms.Dependency(typeof(EnvironmentCheckServiceiOS))]
namespace CameraSlider.Frontend.Forms.iOS.Services
{
    public class EnvironmentCheckServiceiOS : IEnvironmentCheckService
    {
        public bool IsSimulatorOrEmulator()
        {
            if (Runtime.Arch == Arch.SIMULATOR)
                return true;

            return false;
        }
    }
}
