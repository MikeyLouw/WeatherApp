using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.ViewModels
{
	public class YourCitiesPageViewModel : BaseViewModel, INavigationAware
	{
        IFileService fileService;
        IAPIService aPIService;

        List<WeatherApp.Models.City.Welcome> list = new List<Models.City.Welcome>();

        private bool _Loading;
        public bool Loading
        {
            get { return _Loading; }
            set { SetValue(ref _Loading, value); }
        }

        private string _Search;
        public string Search {
            get { return _Search; }
            set {
                if (string.IsNullOrEmpty(value))
                {
                    Cities = list;
                }
                else
                {
                    Cities = list.Where(x => x.Name.ToLower().Contains(value.ToLower())).ToList();
                }
                SetValue(ref _Search, value);
            }
        }

        private List<WeatherApp.Models.City.Welcome> _Cities;
        public List<WeatherApp.Models.City.Welcome> Cities
        {
            get { return _Cities; }
            set { SetValue(ref _Cities, value); }
        }

        public YourCitiesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IFileService fileService, IAPIService aPIService):
            base(navigationService, pageDialogService)
        {
            this.fileService = fileService;
            this.aPIService = aPIService;
        }

        public async void SetupList()
        {
            try
            {
                var stringResult = fileService.ReadFile("City_List");
                list = JsonConvert.DeserializeObject<List<WeatherApp.Models.City.Welcome>>(stringResult);
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

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            Loading = true;
            await DownloadCities();
        }

        public async Task DownloadCities()
        {
            try
            {
                var IsSuccessfulDownload = true;
                fileService.DeleteFile("City_List");
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

                Loading = false;
            }
            catch (Exception ex)
            {
                await pageDialogService.DisplayAlertAsync("Attention", ex.Message, "OK");    
                Loading = false;
            }
        }
    }
}
