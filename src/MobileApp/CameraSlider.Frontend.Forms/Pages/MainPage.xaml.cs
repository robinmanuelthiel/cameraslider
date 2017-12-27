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
using CameraSlider.Frontend.Shared.Models;

namespace CameraSlider.Frontend.Forms.Pages
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;
        private IBluetoothLeService bluetoothLeService;
        private bool isSettingsContainerOpen;

        private const string cameraSliderGuid = "00000000-0000-0000-0000-606405d147b4";
        private const string serviceUuid = "0000ffe0-0000-1000-8000-00805f9b34fb";
        private const string characteristicUuid = "0000ffe1-0000-1000-8000-00805f9b34fb";

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");

            viewModel = App.ServiceLocator.MainPageViewModel;
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

            RotateLeftButton.TouchedDown += RotateLeftButton_TouchedDown;
            RotateRightButton.TouchedDown += RotateRightButton_TouchedDown;
            RotateRightButton.TouchedUp += RotateButton_TouchedUp;
            RotateLeftButton.TouchedUp += RotateButton_TouchedUp;

            await viewModel.TestConnectionAsync(serviceUuid, characteristicUuid);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            if (isSettingsContainerOpen)
            {
                await SlideSettingsDown(false);
            }

            await StopSliderMovement(MotorType.Main);
            await StopSliderMovement(MotorType.HorizontalRotation);

            MoveLeftButton.TouchedDown -= MoveLeftButton_TouchedDown;
            MoveLeftButton.TouchedUp -= MoveButton_TouchedUp;
            MoveRightButton.TouchedDown -= MoveRightButton_TouchedDown;
            MoveRightButton.TouchedUp -= MoveButton_TouchedUp;
            TakePictureButton.TouchedUp -= TakePictureButton_TouchedUp;
            RotateLeftButton.TouchedDown -= RotateLeftButton_TouchedDown;
            RotateRightButton.TouchedDown -= RotateRightButton_TouchedDown;
            RotateRightButton.TouchedUp -= RotateButton_TouchedUp;
            RotateLeftButton.TouchedUp -= RotateButton_TouchedUp;
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
            var pos = viewModel.MenuItems.IndexOf(e.SelectedItem as Shared.Models.MenuItem);
            switch (pos)
            {
                case 0: // Device Selection
                    viewModel.NavigateToDeviceSelectionCommand.Execute(null);
                    break;
                case 1: // Configuration
                    viewModel.NavigateToConfigurationCommand.Execute(null);
                    break;
                default:
                    break;
            }
        }

        #region Slider Controls


        async void TakePictureButton_TouchedUp()
        {
            await bluetoothLeService.WriteToServiceCharacteristicAsync($"et{viewModel.ExposureTime.Milliseconds}#", serviceUuid, characteristicUuid);
            await bluetoothLeService.WriteToServiceCharacteristicAsync("shutter#", serviceUuid, characteristicUuid);
        }

        // Rotation
        async void RotateLeftButton_TouchedDown()
        {
            await StartMotorMovement(MotorType.HorizontalRotation, SliderDirection.Left);
        }
        async void RotateRightButton_TouchedDown()
        {
            await StartMotorMovement(MotorType.HorizontalRotation, SliderDirection.Right);
        }
        async void RotateButton_TouchedUp()
        {
            await StopSliderMovement(MotorType.HorizontalRotation);
        }

        // Movement
        async void MoveLeftButton_TouchedDown()
        {
            await StartMotorMovement(MotorType.Main, SliderDirection.Left);
        }
        async void MoveRightButton_TouchedDown()
        {
            await StartMotorMovement(MotorType.Main, SliderDirection.Right);
        }
        async void MoveButton_TouchedUp()
        {
            await StopSliderMovement(MotorType.Main);
        }

        async Task StartMotorMovement(MotorType motor, SliderDirection direction)
        {
            // Direction
            string directionCommand;
            string motorCommand;

            switch (motor)
            {
                default:
                    directionCommand = direction == SliderDirection.Right ? "dr#" : "dl#";
                    motorCommand = "on#";
                    break;
                case MotorType.HorizontalRotation:
                    directionCommand = direction == SliderDirection.Right ? "hrdr#" : "hrdl#";
                    motorCommand = "hron#";
                    break;
            }

            // Direction
            await bluetoothLeService.WriteToServiceCharacteristicAsync(directionCommand, serviceUuid, characteristicUuid);

            // Speed
            var speedValue = 2500 - (int)SpeedSlider.Value;
            await bluetoothLeService.WriteToServiceCharacteristicAsync($"sp{speedValue}#", serviceUuid, characteristicUuid);

            // Start
            await bluetoothLeService.WriteToServiceCharacteristicAsync(motorCommand, serviceUuid, characteristicUuid);
        }

        async Task StopSliderMovement(MotorType motor)
        {
            string motorCommand;

            switch (motor)
            {
                default:
                    motorCommand = "off#";
                    break;
                case MotorType.HorizontalRotation:
                    motorCommand = "hroff#";
                    break;
            }

            await bluetoothLeService.WriteToServiceCharacteristicAsync(motorCommand, serviceUuid, characteristicUuid);
        }

        #endregion

        #region Settings

        async void SettingsButton_Clicked(object sender, System.EventArgs e)
        {
            SettingsButton.IsEnabled = false;
            Overlay.IsVisible = true;
            await Task.WhenAll(
                Overlay.FadeTo(0.65, 250),
                SettingsContainer.TranslateTo(0, 0, 250, Easing.CubicOut)
            );
            isSettingsContainerOpen = true;
        }

        async void Page_Tapped(object sender, System.EventArgs e)
        {
            if (isSettingsContainerOpen)
                await SlideSettingsDown();
        }

        async Task SlideSettingsDown(bool animate = true)
        {
            if (animate)
            {
                await Task.WhenAll(
                    Overlay.FadeTo(0, 250),
                    SettingsContainer.TranslateTo(0, 300, 250, Easing.CubicOut)
                );
            }
            else
            {
                SettingsContainer.TranslationY = 300;
            }
            Overlay.IsVisible = false;
            isSettingsContainerOpen = false;
            SettingsButton.IsEnabled = true;
        }

        #endregion
    }
}
