using System;
using System.Collections.Generic;
using Plugin.VersionTracking;
using Xamarin.Forms;

namespace CameraSlider.Frontend.Forms.Pages
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");

            var version = CrossVersionTracking.Current;
            VersionText.Text = $"{version.CurrentVersion} (Build {version.CurrentBuild})";
        }
    }
}
