using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CameraSlider.Frontend.Shared.ViewModels;

namespace CameraSlider.Frontend.Forms.Pages
{
    public partial class DeviceSelectionPage : ContentPage
    {
        DeviceSelectionViewModel viewModel;

        public DeviceSelectionPage()
        {
            InitializeComponent();
            viewModel = App.ServiceLocator.DeviceSelectionViewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.RefreshAsync();
        }
    }
}
