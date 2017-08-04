using System;
using CameraSlider.Frontend.Shared.Services;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CameraSlider.Frontend.Shared.Misc;
using System.Threading.Tasks;
using IDialogService = CameraSlider.Frontend.Shared.Services.IDialogService;

namespace CameraSlider.Frontend.Shared.ViewModels
{
    public class MainViewModel : AsyncViewModelBase
    {
        private INavigationService navigationService;
        private IDialogService dialogService;
        private IBluetoothLeService bluetoothLeService;

        public bool isDeviceConnected;
        public bool IsDeviceConnected
        {
            get { return isDeviceConnected; }
            set { isDeviceConnected = value; RaisePropertyChanged(); }
        }


        private string connectionStatus;
        public string ConnectionStatus
        {
            get
            {
                return isDeviceConnected ? "Connected" : "Not connected";
            }
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

        private RelayCommand navigateToConfigurationCommand;
        public RelayCommand NavigateToConfigurationCommand
        {
            get
            {
                return navigateToConfigurationCommand ?? (navigateToConfigurationCommand = new RelayCommand(() =>
                {
                    navigationService.NavigateTo(PageNames.ConfigurationPage);
                }));
            }
        }

        public MainViewModel(INavigationService navigationService, IDialogService dialogService, IBluetoothLeService bluetoothLeService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.bluetoothLeService = bluetoothLeService;
        }

        public async Task TestConnectionAsync(string serviceUuid, string characteristicUuid)
        {
            IsDeviceConnected = false;

            if (bluetoothLeService.ConnectedDevice != null)
            {
                if (await bluetoothLeService.WriteToServiceCharacteristicAsync("test", serviceUuid, characteristicUuid))
                {
                    IsDeviceConnected = true;
                }
                else
                {
                    await dialogService.DisplayDialogAsync("Connection failed", "Could not connect with device. Please check the connection and select the correct device.", "Ok");
                }
            }

            RaisePropertyChanged(nameof(ConnectionStatus));
        }

        public override Task RefreshAsync()
        {
            throw new NotImplementedException();
        }
    }
}
