﻿using Prism;
using Prism.Ioc;
using WeatherApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using WeatherApp.ViewModels;
using WeatherApp.Services.Interfaces;
using WeatherApp.Services.Implementation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WeatherApp
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("/WeatherMasterDetailPage/CustomNavigationPage/YourWeatherPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Navigation
            containerRegistry.RegisterForNavigation<CustomNavigationPage>();
            containerRegistry.RegisterForNavigation<WeatherMasterDetailPage, WeatherMasterDetailViewModel>();
            containerRegistry.RegisterForNavigation<YourWeatherPage, YourWeatherViewModel>();
            containerRegistry.RegisterForNavigation<YourCitiesPage>();

            //Services
            containerRegistry.Register<IHttpClientService, HttpClientService>();
            containerRegistry.Register<IAPIService, APIService>();
            containerRegistry.Register<IGeoService, GeoService>();
        }
    }
}
