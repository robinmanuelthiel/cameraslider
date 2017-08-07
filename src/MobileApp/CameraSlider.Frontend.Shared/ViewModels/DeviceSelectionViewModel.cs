using System;
using System.Threading.Tasks;
using CameraSlider.Frontend.Shared.Services;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using CameraSlider.Frontend.Shared.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IDialogService = CameraSlider.Frontend.Shared.Services.IDialogService;

namespace CameraSlider.Frontend.Shared.ViewModels
{
    public class DeviceSelectionViewModel : AsyncViewModelBase
    {
        private INavigationService navigationService;
        private IDialogService dialogService;
        private IBluetoothLeService bluetoothLeService;

        public ObservableCollection<IBluetoothDevice> bluetoothDevices;
        public ObservableCollection<IBluetoothDevice> BluetoothDevices
        {
            get { return bluetoothDevices; }
            set { bluetoothDevices = value; RaisePropertyChanged(); }
        }


        private RelayCommand<IBluetoothDevice> connectToDeviceCommand;
        public RelayCommand<IBluetoothDevice> ConnectToDeviceCommand
        {
            get
            {
                return connectToDeviceCommand ?? (connectToDeviceCommand = new RelayCommand<IBluetoothDevice>(async (IBluetoothDevice device) =>
                {
                    IsBusy = true;

                    // Try to connect with device
                    await bluetoothLeService.ConnectToDeviceAsync(device);

                    IsBusy = false;

                    // Check if connection has been successfully
                    if (bluetoothLeService.ConnectedDevice != null)
                        navigationService.GoBack();
                    else
                        await dialogService.DisplayDialogAsync("Connection failed", "Could not connect to device.", "Ok");
                }));
            }
        }

        public DeviceSelectionViewModel(INavigationService navigationService, IDialogService dialogService, IBluetoothLeService bluetoothLeService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.bluetoothLeService = bluetoothLeService;

            BluetoothDevices = new ObservableCollection<IBluetoothDevice>();
        }

        public override async Task RefreshAsync()
        {
            IsBusy = true;


            if (bluetoothLeService.GetConnectionStatus())
            {
                var availableBluetoothDevices = await bluetoothLeService.ScanForDevicesAsync();

                BluetoothDevices.Clear();
                foreach (var device in availableBluetoothDevices)
                {
                    BluetoothDevices.Add(device);
                }
            }
            else
            {
                await dialogService.DisplayDialogAsync("Bluetooth not available", "BluetoothLE is not available. Please check your BLuetooth settings and try again", "Ok");
            }

            IsBusy = false;
        }
    }
}
