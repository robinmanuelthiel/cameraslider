using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using CameraSlider.Frontend.Shared.Services;

namespace CameraSlider.Frontend.Shared.ViewModels
{
    public class ConfigurationViewModel : AsyncViewModelBase
    {
        private IDialogService dialogService;

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

        private int exposureTime;
        public int ExposureTime
        {
            get { return exposureTime; }
            set { exposureTime = value; RaisePropertyChanged(); }
        }

        private RelayCommand sendToSliderCommand;
        public RelayCommand SendToSliderCommand
        {
            get
            {
                return sendToSliderCommand ?? (sendToSliderCommand = new RelayCommand(() =>
                {
                    dialogService.DisplayDialogAsync("", $"Shots: {NumberOfShots}\nInterval: {Interval}\nExposure: {ExposureTime}", "Aha");
                }));
            }
        }

        public ConfigurationViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            // Set defaults
            numberOfShotsOptions = new int[] { 1, 2, 3, 4, 5 };
            numberOfShots = 5;
            Interval = 2;
            ExposureTime = 1;
        }

        public override Task RefreshAsync()
        {
            throw new NotImplementedException();
        }
    }
}
