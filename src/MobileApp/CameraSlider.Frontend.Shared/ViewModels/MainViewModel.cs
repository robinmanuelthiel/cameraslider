using System;
using CameraSlider.Frontend.Shared.Services;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CameraSlider.Frontend.Shared.Misc;
using System.Threading.Tasks;

namespace CameraSlider.Frontend.Shared.ViewModels
{
    public class MainViewModel : AsyncViewModelBase
    {
        private INavigationService navigationService;
        private IBluetoothLeService bluetoothLeService;

        public bool isDeviceConnected;
        public bool IsDeviceConnected
        {
            get { return isDeviceConnected; }
            set { isDeviceConnected = value; RaisePropertyChanged(); }
        }

        private RelayCommand navigateToDeviceSelectionCommand;
        public RelayCommand NavigateToDeviceSelectionCommand
        {
            get
            {
                return navigateToDeviceSelectionCommand ?? (navigateToDeviceSelectionCommand = new RelayCommand(() =>
                {
                    navigationService.NavigateTo(PageNames.DeviceSelectionPage);
                }));
            }
        }

        public MainViewModel(INavigationService navigationService, IBluetoothLeService bluetoothLeService)
        {
            this.navigationService = navigationService;
            this.bluetoothLeService = bluetoothLeService;
        }

        public override async Task RefreshAsync()
        {
            IsDeviceConnected = bluetoothLeService.ConnectedDevice != null;
        }
    }
}
