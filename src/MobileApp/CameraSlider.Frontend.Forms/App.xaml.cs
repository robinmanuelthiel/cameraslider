using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CameraSlider.Frontend.Forms.Pages;
using Plugin.DeviceInfo;
using Plugin.VersionTracking;
using Xamarin.Forms;
using CameraSlider.Frontend.Forms.Services;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Push;
using GalaSoft.MvvmLight.Ioc;
using CameraSlider.Frontend.Shared.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CameraSlider.Frontend.Forms
{
    public partial class App : Application
    {
        public static ServiceLocator ServiceLocator;

        public App()
        {
            InitializeComponent();

            // Initialize Service Locator
            ServiceLocator = new ServiceLocator();

            // Create Naviagation Page
            var navigationPage = new Xamarin.Forms.NavigationPage();
            navigationPage.BarBackgroundColor = (Color)Resources["AccentColor"];
            navigationPage.BackgroundColor = (Color)Resources["BackgroundColor"];
            navigationPage.BarTextColor = Color.White;
            navigationPage.On<iOS>().SetPrefersLargeTitles(true);

            // Initialize NavigationService using the navigation page
            ServiceLocator.RegisterNavigationService(navigationPage);

            // Add MainPage as Navigation root afterwards, as MainPage constructor need to have the 
            // NavigationService initialized
            //var root = navigationPage.Navigation.NavigationStack[0];
            navigationPage.Navigation.PushAsync(new MainPage());

            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
            CrossVersionTracking.Current.Track();

            // Enable Mobile Center only if not running on Simulator/Emulator or in Debug Mode
            var environment = DependencyService.Get<IEnvironmentService>();
            if (environment?.IsRunningInRealWorld() == false)
            {
                Analytics.SetEnabledAsync(false);
                Crashes.SetEnabledAsync(false);
                Distribute.SetEnabledAsync(false);
            }

            AppCenter.Start(
                "ios=177833a8-ea1c-4ceb-b784-83330ca933b7;" +
                "android=0036cb00-df40-4131-a919-6e5a83b0371c;",
                typeof(Analytics),
                typeof(Crashes),
                typeof(Distribute),
                typeof(Push));

            Push.PushNotificationReceived += async (object sender, PushNotificationReceivedEventArgs e) =>
            {
                var dialogService = SimpleIoc.Default.GetInstance<IDialogService>();
                await dialogService?.DisplayDialogAsync(e.Title, e.Message, "Ok");
            };
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
