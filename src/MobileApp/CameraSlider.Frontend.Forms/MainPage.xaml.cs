using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CameraSlider.Frontend.Forms
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();

            MoveLeftButton.TouchedDown += MoveLeftButton_TouchedDown;
            MoveLeftButton.TouchedUp += MoveButton_TouchedUp;
            MoveRightButton.TouchedDown += MoveRightButton_TouchedDown;
            MoveRightButton.TouchedUp += MoveButton_TouchedUp;
            TakePictureButton.TouchedUp += TakePictureButton_TouchedUp;
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
            StartSliderMovement(SliderDirection.Right);
        }

        void MoveRightButton_TouchedDown()
        {
            StartSliderMovement(SliderDirection.Left);
        }

        void MoveButton_TouchedUp()
        {
            StopSliderMovement();
        }

        void StartSliderMovement(SliderDirection direction)
        {
            //throw new NotImplementedException();
        }

        void StopSliderMovement()
        {
            //throw new NotImplementedException();
        }
    }
}
