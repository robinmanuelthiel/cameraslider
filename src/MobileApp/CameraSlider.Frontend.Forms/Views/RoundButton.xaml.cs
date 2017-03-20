﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CameraSlider.Frontend.Forms.Views
{
    public partial class RoundButton : ContentView
    {
        public RoundButton()
        {
            InitializeComponent();

            TouchRecognizer.TouchDown += TouchDown;
            TouchRecognizer.TouchUp += TouchUp;

        }

        private void TouchDown()
        {
            Container.MinimumHeightRequest = 50;
            Container.BackgroundColor = Color.FromHex("279396");
        }

        private void TouchUp()
        {
            Container.BackgroundColor = Color.Gray;
        }
    }
}
