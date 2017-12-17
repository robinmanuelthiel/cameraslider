using System;
using System.Collections.Generic;
using CameraSlider.Frontend.Shared.Models;
using Plugin.VersionTracking;
using Xamarin.Forms;

namespace CameraSlider.Frontend.Forms.Pages
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = App.ServiceLocator.AboutPageViewModel;

            // Set Version and Build number
            VersionLabel.Text = $"{CrossVersionTracking.Current.CurrentVersion} (Build {CrossVersionTracking.Current.CurrentBuild})";

        }

        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is ThirdPartyLibrary selectedLibrary)
                Device.OpenUri(new Uri(selectedLibrary.Url));

            (sender as ListView).SelectedItem = null;
        }
    }
}