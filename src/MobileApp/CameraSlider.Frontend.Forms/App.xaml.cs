using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CameraSlider.Frontend.Forms.Pages;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Distribute;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
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
            var navigationPage = new NavigationPage();

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
#if !DEBUG
            MobileCenter.Start(
                "android=f7090947-7e5b-4e61-9adf-4003961fa537;",
                typeof(Analytics),
                typeof(Crashes),
                typeof(Distribute));
#endif
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
