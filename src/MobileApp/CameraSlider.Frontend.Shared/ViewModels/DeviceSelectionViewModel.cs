using System;
using System.Threading.Tasks;
using CameraSlider.Frontend.Shared.Services;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using CameraSlider.Frontend.Shared.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IDialogService = CameraSlider.Frontend.Shared.Services.IDialogService;
using MvvmHelpers;
using CameraSlider.Frontend.Shared.Abstractions;

namespace CameraSlider.Frontend.Shared.ViewModels
{
    public class DeviceSelectionViewModel : AsyncViewModelBase
    {
        private INavigationService navigationService;
        private IDialogService dialogService;
        private IBluetoothLeService bluetoothLeService;

        public ObservableRangeCollection<IBluetoothDevice> bluetoothDevices;
        public ObservableRangeCollection<IBluetoothDevice> BluetoothDevices
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

            BluetoothDevices = new ObservableRangeCollection<IBluetoothDevice>();
        }

        public override async Task RefreshAsync()
        {
            IsBusy = true;

            // Check BluetoothLE connection status
            if (bluetoothLeService.GetConnectionStatus())
            {
                // Get a list of BluetoothLE devices
                BluetoothDevices.Clear();
                await bluetoothLeService.ScanForDevicesAsync(BluetoothDevices);
                //BluetoothDevices.ReplaceRange(availableBluetoothDevices);

                IsBusy = false;
            }
            else
            {
                IsBusy = false;

                // Bluetooth LE not available
                await dialogService.DisplayDialogAsync("Bluetooth not available", "Bluetooth is not available. Please check your Bluetooth settings and try again", "Ok");
            }
        }
    }
}
