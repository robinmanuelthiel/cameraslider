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

namespace CameraSlider.Frontend.Forms
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel;

        private const string cameraSliderGuid = "00000000-0000-0000-0000-606405d147b4";
        private const string serviceUuid = "0000ffe0-0000-1000-8000-00805f9b34fb";
        private const string characteristicUuid = "0000ffe1-0000-1000-8000-00805f9b34fb";

        public MainPage()
        {
            InitializeComponent();
            viewModel = App.ServiceLocator.MainViewModel;
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
            await viewModel.RefreshAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            StopSliderMovement();

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

        void MoveLeftButton_TouchedDown()
        {
            StartSliderMovement(SliderDirection.Left);
        }

        void MoveRightButton_TouchedDown()
        {
            StartSliderMovement(SliderDirection.Right);
        }

        void MoveButton_TouchedUp()
        {
            StopSliderMovement();
        }

        async void StartSliderMovement(SliderDirection direction)
        {
            var bluetoothLeService = SimpleIoc.Default.GetInstance<IBluetoothLeService>();

            if (direction == SliderDirection.Right)
                await bluetoothLeService.WriteToServiceCharacteristicAsync("on#", serviceUuid, characteristicUuid);
            else
                await bluetoothLeService.WriteToServiceCharacteristicAsync("off#", serviceUuid, characteristicUuid);
        }

        void StopSliderMovement()
        {
            //throw new NotImplementedException();
        }
    }
}
