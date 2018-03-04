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

        string DegreeSymbol = "&#186;";
        string BaseUrlImage = "http://openweathermap.org/img/w/";

        private string _Date;
        public string Date{
            get { return _Date; }
            set { SetProperty(ref _Date, value); }
        }

        private ImageSource _WeatherImage;
        public ImageSource WeatherImage{
            get { return _WeatherImage; }
            set { SetProperty(ref _WeatherImage, value); }
        }

        private string _MaxTemp;
        public string MaxTemp{
            get { return _MaxTemp; }
            set { SetProperty(ref _MaxTemp, value); }
        }

        private string _MinTemp;
        public string MinTemp{
            get { return _MinTemp; }
            set { SetProperty(ref _MinTemp, value); }
        }

        private string _Location;
        public string Location{
            get { return _Location; }
            set { SetProperty(ref _Location, value); }
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

            Task.Run(async () => { await this.LoadWeatherData(); });
        }

        public async Task LoadWeatherData(){
            try
            {
                var result = await this.aPIService.GetWeather(new Models.Weather.Welcome());

                Date = "TODAY, " + DateTime.Today.ToString("MMMM");
                WeatherImage = ImageSource.FromUri(new Uri(BaseUrlImage + result.Weather.FirstOrDefault().Icon + ".png"));
                MaxTemp = "max " + result.Main.TempMax + DegreeSymbol + "C";
                MaxTemp = "min " + result.Main.TempMin + DegreeSymbol + "C";
                Location = result.Name + ", ";
                Location += await this.geoService.GetCountryName();
            }
            catch (Exception ex)
            {
                await pageDialogService.DisplayAlertAsync("Attention", ex.Message, "OK");
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
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
