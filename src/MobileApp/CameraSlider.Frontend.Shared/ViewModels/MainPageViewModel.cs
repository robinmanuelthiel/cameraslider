using System;
using CameraSlider.Frontend.Shared.Services;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CameraSlider.Frontend.Shared.Misc;
using System.Threading.Tasks;
using IDialogService = CameraSlider.Frontend.Shared.Services.IDialogService;
using System.Collections.ObjectModel;
using CameraSlider.Frontend.Shared.Models;
using System.Linq;

namespace CameraSlider.Frontend.Shared.ViewModels
{
    public class MainPageViewModel : AsyncViewModelBase
    {
        private INavigationService navigationService;
        private IDialogService dialogService;
        private IBluetoothLeService bluetoothLeService;

        private ObservableCollection<MenuItem> menuItems;
        public ObservableCollection<MenuItem> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ExposureTime> exposureTimeOptions;
        public ObservableCollection<ExposureTime> ExposureTimeOptions
        {
            get { return exposureTimeOptions; }
            set { exposureTimeOptions = value; RaisePropertyChanged(); }
        }

        private ExposureTime exposureTime;
        public ExposureTime ExposureTime
        {
            get { return exposureTime; }
            set { exposureTime = value; RaisePropertyChanged(); }
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

        private RelayCommand navigateToAboutCommand;
        public RelayCommand NavigateToAboutCommand
        {
            get
            {
                return navigateToAboutCommand ?? (navigateToAboutCommand = new RelayCommand(() =>
                {
                    navigationService.NavigateTo(PageNames.AboutPage);
                }));
            }
        }

        public MainPageViewModel(INavigationService navigationService, IDialogService dialogService, IBluetoothLeService bluetoothLeService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.bluetoothLeService = bluetoothLeService;

            menuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem("Status", "Not connected", ""),
                new MenuItem("Configuration", "Not configured", ""),
            };
            exposureTimeOptions = new ObservableCollection<ExposureTime>(ExposureTime.Times);
            exposureTime = exposureTimeOptions.FirstOrDefault(e => e.Milliseconds == 8);
        }

        public async Task TestConnectionAsync(string serviceUuid, string characteristicUuid)
        {
            // Test Device connetion
            if (bluetoothLeService.ConnectedDevice != null && await bluetoothLeService.WriteToServiceCharacteristicAsync("test#", serviceUuid, characteristicUuid))
            {
                MenuItems.First().Subtitle = "Connected";
            }
            else
            {
                MenuItems.First().Subtitle = "Not connected";
            }
        }

        public override Task RefreshAsync()
        {
            return null;
        }
    }
}
