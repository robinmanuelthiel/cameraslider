using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using CameraSlider.Frontend.Shared.Services;
using CameraSlider.Frontend.Shared.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace CameraSlider.Frontend.Shared.ViewModels
{
    public class ConfigurationViewModel : AsyncViewModelBase
    {
        private IDialogService dialogService;
        private IBluetoothLeService bluetoothLeService;

        private int[] numberOfShotsOptions;
        public int[] NumberOfShotsOptions
        {
            get { return numberOfShotsOptions; }
            set { numberOfShotsOptions = value; RaisePropertyChanged(); }
        }

        private int numberOfShots;
        public int NumberOfShots
        {
            get { return numberOfShots; }
            set { numberOfShots = value; RaisePropertyChanged(); }
        }

        private int interval;
        public int Interval
        {
            get { return interval; }
            set { interval = value; RaisePropertyChanged(); }
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

        private RelayCommand sendToSliderCommand;
        public RelayCommand SendToSliderCommand
        {
            get
            {
                return sendToSliderCommand ?? (sendToSliderCommand = new RelayCommand(async () =>
                {
                    if (await dialogService.DisplayDialogAsync("Procedure", $"Shots: {NumberOfShots}\nInterval: {Interval}\nExposure: {ExposureTime.Milliseconds}", "Start", "Cancel"))
                    {
                        string serviceUuid = "0000ffe0-0000-1000-8000-00805f9b34fb";
                        string characteristicUuid = "0000ffe1-0000-1000-8000-00805f9b34fb";

                        var stepsPerInterval = Interval;

                        await bluetoothLeService.WriteToServiceCharacteristicAsync($"pr{stepsPerInterval},{NumberOfShots}#", serviceUuid, characteristicUuid);
                    }
                }));
            }
        }

        public ConfigurationViewModel(IDialogService dialogService, IBluetoothLeService bluetoothLeService)
        {
            this.dialogService = dialogService;
            this.bluetoothLeService = bluetoothLeService;

            exposureTimeOptions = new ObservableCollection<ExposureTime>(ExposureTime.Times);
            exposureTime = exposureTimeOptions.FirstOrDefault();
            numberOfShotsOptions = new int[] { 1, 2, 3, 4, 5 };
            numberOfShots = 5;
            Interval = 2;
        }

        public override Task RefreshAsync()
        {
            throw new NotImplementedException();
        }
    }
}
