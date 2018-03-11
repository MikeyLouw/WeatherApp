using System;
using WeatherApp.Services.Interfaces;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Prism.Mvvm;
using System.Xml;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using WeatherApp.Settings;

namespace WeatherApp.ViewModels
{
    public class YourWeatherViewModel : BaseViewModel, INavigationAware
    {
        IAPIService aPIService;
        IFileService fileService;
        IGeoService geoService;

        readonly string DegreeSymbol = "°";
        readonly string BaseUrlImage = Constants.URL_WEATHERICON;

        private bool _Loading;
        public bool Loading
        {
            get { return _Loading; }
            set { SetValue(ref _Loading, value); }
        }

        private string _Date;
        public string Date{
            get { return _Date; }
            set { SetValue(ref _Date, value); }
        }

        private ImageSource _WeatherImage;
        public ImageSource WeatherImage{
            get { return _WeatherImage; }
            set { SetValue(ref _WeatherImage, value); }
        }

        private string _MaxTemp;
        public string MaxTemp{
            get { return _MaxTemp; }
            set { SetValue(ref _MaxTemp, value); }
        }

        private string _MinTemp;
        public string MinTemp{
            get { return _MinTemp; }
            set { SetValue(ref _MinTemp, value); }
        }

        private string _Location;
        public string Location{
            get { return _Location; }
            set { SetValue(ref _Location, value); }
        }

        public YourWeatherViewModel(INavigationService navigationService, 
                                    IPageDialogService pageDialogService, 
                                    IAPIService aPIService,
                                    IFileService fileService,
                                    IGeoService geoService):
        base (navigationService, pageDialogService)
        {
            this.aPIService = aPIService;
            this.fileService = fileService;
            this.geoService = geoService;
        }

        public async Task LoadWeatherData()
        {
            try
            {
                Loading = true;

                var result = await this.aPIService.GetWeather<Models.Weather.Welcome>();
                var CountryName = await this.geoService.GetCountryName();

                Date = string.Format("TODAY, {0} {1} {2}", new object[3] { DateTime.Today.Day, DateTime.Today.ToString("MMMM"), DateTime.Today.Year});
                WeatherImage = ImageSource.FromUri(new Uri(BaseUrlImage + result.Weather.FirstOrDefault().Icon + ".png"));
                MaxTemp = string.Format("max {0}{1}C", new object[2] { result.Main.TempMax, DegreeSymbol });
                MinTemp = string.Format("min {0}{1}C", new object[2] { result.Main.TempMin, DegreeSymbol });
                Location = string.Format("{0}, {1}", new object[2] { result.Name, CountryName });
            }
            catch (Exception ex)
            {
                await pageDialogService.DisplayAlertAsync("Attention", ex.Message, "OK");
            }
            finally
            {
                Loading = false;
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await this.LoadWeatherData();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
