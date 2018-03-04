using System;
using WeatherApp.Services.Interfaces;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;

namespace WeatherApp.ViewModels
{
    public class YourWeatherViewModel : BaseViewModel
    {
        IAPIService aPIService;

        public YourWeatherViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IAPIService aPIService):
        base (navigationService, pageDialogService)
        {
            this.aPIService = aPIService;
        }

        public async Task LoadWeatherData(){
            try
            {
                var result = await this.aPIService.GetWeather(new Models.Weather.Welcome());

            }
            catch (Exception ex)
            {
                await pageDialogService.DisplayAlertAsync("Attention", ex.Message, "OK");
            }
        }
    }
}
