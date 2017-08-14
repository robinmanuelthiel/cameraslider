using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using CameraSlider.Frontend.Forms.Services;
using CameraSlider.Frontend.Shared.Services;
using Plugin.BLE.Abstractions.Contracts;
using CameraSlider.Frontend.Shared.ViewModels;
using CameraSlider.Frontend.Shared.Misc;
using CameraSlider.Frontend.Forms.Pages;

namespace CameraSlider.Frontend.Forms
{
    public class ServiceLocator
    {
        public ServiceLocator()
        {
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Reset();

            // Register Services
            SimpleIoc.Default.Register<IBluetoothLeService, BluetoothLeService>();
            SimpleIoc.Default.Register<Shared.Services.IDialogService, DialogService>();

            // Register View Models
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DeviceSelectionViewModel>();
            SimpleIoc.Default.Register<ConfigurationViewModel>();
        }

        public MainViewModel MainViewModel { get { return SimpleIoc.Default.GetInstance<MainViewModel>(); } }
        public DeviceSelectionViewModel DeviceSelectionViewModel { get { return SimpleIoc.Default.GetInstance<DeviceSelectionViewModel>(); } }
        public ConfigurationViewModel ConfigurationViewModel { get { return SimpleIoc.Default.GetInstance<ConfigurationViewModel>(); } }

        public void RegisterNavigationService(NavigationPage navigationPage)
        {
            var navigationService = new NavigationService(navigationPage);
            navigationService.Configure(PageNames.MainPage, typeof(MainPage));
            navigationService.Configure(PageNames.DeviceSelectionPage, typeof(DeviceSelectionPage));
            navigationService.Configure(PageNames.ConfigurationPage, typeof(ConfigurationPage));
            navigationService.Configure(PageNames.AboutPage, typeof(AboutPage));

            if (SimpleIoc.Default.IsRegistered<INavigationService>())
                SimpleIoc.Default.Unregister<INavigationService>();

            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
        }
    }
}
