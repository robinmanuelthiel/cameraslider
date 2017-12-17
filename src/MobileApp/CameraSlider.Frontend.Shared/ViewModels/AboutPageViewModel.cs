using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CameraSlider.Frontend.Shared.Models;

namespace CameraSlider.Frontend.Shared.ViewModels
{
    public class AboutPageViewModel : AsyncViewModelBase
    {
        private ObservableCollection<ThirdPartyLibrary> thirdPartyLibraries;
        public ObservableCollection<ThirdPartyLibrary> ThirdPartyLibraries
        {
            get { return thirdPartyLibraries; }
            set { thirdPartyLibraries = value; RaisePropertyChanged(); }
        }

        public AboutPageViewModel()
        {
            ThirdPartyLibraries = new ObservableCollection<ThirdPartyLibrary>
            {
                new ThirdPartyLibrary("MVVM Light", "Laurent Bugnion", "http://www.mvvmlight.net", Platform.Xamarin),
                new ThirdPartyLibrary("MVVM Helpers", "James Montemagno", "https://github.com/jamesmontemagno/mvvm-helpers", Platform.Xamarin),
                new ThirdPartyLibrary("Version Tracking Plugin", "Colby L. Williams", "https://github.com/colbylwilliams/VersionTrackingPlugin", Platform.Xamarin),
                new ThirdPartyLibrary("Bluetooth LE plugin for Xamarin", "Adrian Seceleanu & Sven-Michael Stübe", "https://github.com/xabre/xamarin-bluetooth-le", Platform.Xamarin)
            };
        }

        public override Task RefreshAsync()
        {
            throw new NotImplementedException();
        }
    }
}
