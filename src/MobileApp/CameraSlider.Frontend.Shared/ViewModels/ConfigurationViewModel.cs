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

        // Procedure Values
        private readonly int bufferTime = 250;
        private readonly int speed = 1;
        private int stepsPerInterval;
        private int horizontalRotationStepsPerInterval;

        private int numberOfShots;
        public int NumberOfShots
        {
            get { return numberOfShots; }
            set { numberOfShots = value; RaisePropertyChanged(); CalculateProcedureValues(); }
        }

        private int interval;
        public int Interval
        {
            get { return interval; }
            set { interval = value; RaisePropertyChanged(); CalculateProcedureValues(); }
        }

        private int totalSteps;
        public int TotalSteps
        {
            get { return totalSteps; }
            set { totalSteps = value; RaisePropertyChanged(); CalculateProcedureValues(); }
        }

        private int horizontalRotationSteps;
        public int HorizontalRotationSteps
        {
            get { return horizontalRotationSteps; }
            set { horizontalRotationSteps = value; RaisePropertyChanged(); CalculateProcedureValues(); }
        }

        private ObservableCollection<SliderDirection> directionOptions;
        public ObservableCollection<SliderDirection> DirectionOptions
        {
            get { return directionOptions; }
            set { directionOptions = value; RaisePropertyChanged(); }
        }

        private SliderDirection direction;
        public SliderDirection Direction
        {
            get { return direction; }
            set { direction = value; RaisePropertyChanged(); }
        }

        private SliderDirection rotationDirection;
        public SliderDirection RotationDirection
        {
            get { return rotationDirection; }
            set { rotationDirection = value; RaisePropertyChanged(); }
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
            set { exposureTime = value; RaisePropertyChanged(); CalculateProcedureValues(); }
        }

        private int maxExposureTime;
        public int MaxExposureTime
        {
            get { return maxExposureTime; }
            set { maxExposureTime = value; RaisePropertyChanged(); }
        }

        private RelayCommand sendToSliderCommand;
        public RelayCommand SendToSliderCommand
        {
            get
            {
                return sendToSliderCommand ?? (sendToSliderCommand = new RelayCommand(async () =>
                {
                    CalculateProcedureValues();

                    if (await dialogService.DisplayDialogAsync("Procedure", $"Shots: {NumberOfShots}\nInterval: {Interval}\nExposure: {ExposureTime.Milliseconds}\nSteps per Interval: {stepsPerInterval}\nMax Exposure Time: {maxExposureTime}", "Start", "Cancel"))
                    {
                        string serviceUuid = "0000ffe0-0000-1000-8000-00805f9b34fb";
                        string characteristicUuid = "0000ffe1-0000-1000-8000-00805f9b34fb";

                        // Send Direction
                        var directionCommand = direction == SliderDirection.Right ? "dr#" : "dl#";
                        await bluetoothLeService.WriteToServiceCharacteristicAsync(directionCommand, serviceUuid, characteristicUuid);
                        var rotationDirectionCommand = rotationDirection == SliderDirection.Right ? "hrdr#" : "hrdl#";
                        await bluetoothLeService.WriteToServiceCharacteristicAsync(rotationDirectionCommand, serviceUuid, characteristicUuid);

                        // Send Exposure Time
                        await bluetoothLeService.WriteToServiceCharacteristicAsync($"et{ExposureTime.Milliseconds}#", serviceUuid, characteristicUuid);

                        // Send Procedure
                        await bluetoothLeService.WriteToServiceCharacteristicAsync($"pr{stepsPerInterval},{horizontalRotationStepsPerInterval},{NumberOfShots},{maxExposureTime}#", serviceUuid, characteristicUuid);
                    }
                }, () => MaxExposureTime >= 0));
            }
        }

        public ConfigurationViewModel(IDialogService dialogService, IBluetoothLeService bluetoothLeService)
        {
            this.dialogService = dialogService;
            this.bluetoothLeService = bluetoothLeService;

            exposureTimeOptions = new ObservableCollection<ExposureTime>(ExposureTime.Times);
            directionOptions = new ObservableCollection<SliderDirection>(Enum.GetValues(typeof(SliderDirection)).Cast<SliderDirection>());

            // Set defaults
            exposureTime = exposureTimeOptions.FirstOrDefault(e => e.Milliseconds == 8);
            direction = directionOptions.FirstOrDefault();
            totalSteps = 10000;
            numberOfShots = 10;
            Interval = 5;
        }

        public override Task RefreshAsync()
        {
            throw new NotImplementedException();
        }

        private void CalculateProcedureValues()
        {
            stepsPerInterval = TotalSteps / NumberOfShots;
            horizontalRotationStepsPerInterval = horizontalRotationSteps / NumberOfShots;
            MaxExposureTime = (Interval * 1000) - (2 * bufferTime) - ExposureTime.Milliseconds - (stepsPerInterval * speed * 2);
            SendToSliderCommand.RaiseCanExecuteChanged();
        }
    }
}
