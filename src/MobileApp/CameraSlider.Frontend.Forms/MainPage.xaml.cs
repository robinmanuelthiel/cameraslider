using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using CameraSlider.Frontend.Forms.Services;
using CameraSlider.Frontend.Shared.ViewModels;
using CameraSlider.Frontend.Shared.Services;
using GalaSoft.MvvmLight.Ioc;
using CameraSlider.Frontend.Forms.Pages;

namespace CameraSlider.Frontend.Forms
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel;
        private IBluetoothLeService bluetoothLeService;

        private const string cameraSliderGuid = "00000000-0000-0000-0000-606405d147b4";
        private const string serviceUuid = "0000ffe0-0000-1000-8000-00805f9b34fb";
        private const string characteristicUuid = "0000ffe1-0000-1000-8000-00805f9b34fb";

        public MainPage()
        {
            InitializeComponent();

            viewModel = App.ServiceLocator.MainViewModel;
            bluetoothLeService = SimpleIoc.Default.GetInstance<IBluetoothLeService>();

            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            MoveLeftButton.TouchedDown += MoveLeftButton_TouchedDown;
            MoveLeftButton.TouchedUp += MoveButton_TouchedUp;
            MoveRightButton.TouchedDown += MoveRightButton_TouchedDown;
            MoveRightButton.TouchedUp += MoveButton_TouchedUp;
            TakePictureButton.TouchedUp += TakePictureButton_TouchedUp;

            await viewModel.TestConnectionAsync(serviceUuid, characteristicUuid);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            await StopSliderMovement();

            MoveLeftButton.TouchedDown -= MoveLeftButton_TouchedDown;
            MoveLeftButton.TouchedUp -= MoveButton_TouchedUp;
            MoveRightButton.TouchedDown -= MoveRightButton_TouchedDown;
            MoveRightButton.TouchedUp -= MoveButton_TouchedUp;
            TakePictureButton.TouchedUp -= TakePictureButton_TouchedUp;

        }

        void TakePictureButton_TouchedUp()
        {
            DisplayAlert("Camera", "Picture taken!", "Ok");
        }

        async void MoveLeftButton_TouchedDown()
        {
            SpeedSlider.IsEnabled = false;
            await StartSliderMovement(SliderDirection.Left);
        }

        async void MoveRightButton_TouchedDown()
        {
            SpeedSlider.IsEnabled = false;
            await StartSliderMovement(SliderDirection.Right);
        }

        async void MoveButton_TouchedUp()
        {
            await StopSliderMovement();
            SpeedSlider.IsEnabled = true;
        }

        async Task StartSliderMovement(SliderDirection direction)
        {
            // Direction
            var directionCommand = direction == SliderDirection.Right ? "dr#" : "dl#";
            await bluetoothLeService.WriteToServiceCharacteristicAsync(directionCommand, serviceUuid, characteristicUuid);

            // Speed
            var speedValue = 1000 - SpeedSlider.Value;
            await bluetoothLeService.WriteToServiceCharacteristicAsync($"sp{speedValue}#", serviceUuid, characteristicUuid);

            // Start
            await bluetoothLeService.WriteToServiceCharacteristicAsync("on#", serviceUuid, characteristicUuid);

        }

        async Task StopSliderMovement()
        {
            await bluetoothLeService.WriteToServiceCharacteristicAsync("off#", serviceUuid, characteristicUuid);
        }
    }
}
