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

namespace WeatherApp.ViewModels
{
    public class YourWeatherViewModel : BaseViewModel, INavigationAware
    {
        IAPIService aPIService;
        IFileService fileService;
        IGeoService geoService;

        string DegreeSymbol = "°";
        string BaseUrlImage = "http://openweathermap.org/img/w/";

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

        public async Task LoadWeatherData() {
            try
            {
                var result = await this.aPIService.GetWeather<Models.Weather.Welcome>();
                var CountryName = await this.geoService.GetCountryName();

                Date = "TODAY, " + DateTime.Today.Day + " " + DateTime.Today.ToString("MMMM") + " " + DateTime.Today.Year;
                WeatherImage = ImageSource.FromUri(new Uri(BaseUrlImage + result.Weather.FirstOrDefault().Icon + ".png"));
                MaxTemp = "max " + result.Main.TempMax + DegreeSymbol + "C";
                MinTemp = "min " + result.Main.TempMin + DegreeSymbol + "C";
                Location = result.Name + ", " + CountryName;
            }
            catch (Exception ex)
            {
                await pageDialogService.DisplayAlertAsync("Attention", ex.Message, "OK");
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

        public async void DownloadCities(){
            var IsSuccessfulDownload = true;
            if (!fileService.FileExists("City_List"))
            {
                IsSuccessfulDownload = await this.aPIService.GetLatestCities();
            }

            if (IsSuccessfulDownload)
            {
                var ReadStringFromFileSystem = this.fileService.ReadFile("City_List");
                var Cities = JsonConvert.DeserializeObject<List<WeatherApp.Models.City.Welcome>>(ReadStringFromFileSystem);

                var GetCityName = Cities.Where(x => x.Id == 0).FirstOrDefault().Name;
            }
        }
    }
}
