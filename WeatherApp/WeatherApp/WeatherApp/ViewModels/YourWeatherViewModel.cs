using System;
using WeatherApp.Services.Interfaces;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace WeatherApp.ViewModels
{
    public class YourWeatherViewModel : BaseViewModel, INavigationAware
    {
        IAPIService aPIService;
        IFileService fileService;

        public YourWeatherViewModel(INavigationService navigationService, 
                                    IPageDialogService pageDialogService, 
                                    IAPIService aPIService,
                                    IFileService fileService):
        base (navigationService, pageDialogService)
        {
            this.aPIService = aPIService;

            Task.Run(async () => { await this.LoadWeatherData(); });
        }

        public async Task LoadWeatherData(){
            try
            {
                var result = await this.aPIService.GetWeather(new Models.Weather.Welcome());

                var IsSuccessfulDownload = true;
                if (!fileService.FileExists("City_List")){
                    IsSuccessfulDownload = await this.aPIService.GetLatestCities();
                }

                if (IsSuccessfulDownload){
                    var ReadStringFromFileSystem = this.fileService.ReadFile("City_List");
                    var Cities = JsonConvert.DeserializeObject<List<WeatherApp.Models.City.Welcome>>(ReadStringFromFileSystem);

                    var GetCityName = Cities.Where(x => x.Id == result.Id).FirstOrDefault().Name;
                }
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
    }
}
