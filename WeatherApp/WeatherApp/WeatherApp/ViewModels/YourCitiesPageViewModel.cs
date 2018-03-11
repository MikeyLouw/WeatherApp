using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Exceptions.FileExceptions;
using WeatherApp.Services.Interfaces;
using WeatherApp.Settings;

namespace WeatherApp.ViewModels
{
	public class YourCitiesPageViewModel : BaseViewModel, INavigationAware
	{
        IFileService fileService;
        IAPIService aPIService;

        ObservableCollection<WeatherApp.Models.City.Welcome> list;

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
                    search(value);
                }
                SetValue(ref _Search, value);
            }
        }

        private async void search(string value)
        {
            if (Cities != null && list != null && list.Count > 0 && Cities.Count > 0)
            Cities = new ObservableCollection<Models.City.Welcome>(list.Where(x => x.Name.ToLower().Contains(value.ToLower())).OrderByDescending(x => x.Country == "ZA"));
        }

        private ObservableCollection<WeatherApp.Models.City.Welcome> _Cities;
        public ObservableCollection<WeatherApp.Models.City.Welcome> Cities
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            await this.LoadCities();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public async Task LoadCities()
        {
            try
            {
                Loading = true;

                if (fileService.FileExists(Constants.FILE_NAME))
                {
                    var DeserializedObject = JsonConvert.DeserializeObject<List<WeatherApp.Models.City.Welcome>>(await fileService.ReadFile(Constants.FILE_NAME));
                    list = new ObservableCollection<Models.City.Welcome>(DeserializedObject);
                    Cities = new ObservableCollection<Models.City.Welcome>(DeserializedObject);
                }
                else
                {
                    await aPIService.GetLatestCities();
                    var ReadStringFromFileSystem = await this.fileService.ReadFile(Constants.FILE_NAME);
                    var DeserializedObject = JsonConvert.DeserializeObject<List<WeatherApp.Models.City.Welcome>>(ReadStringFromFileSystem);
                    Cities = new ObservableCollection<WeatherApp.Models.City.Welcome>(DeserializedObject.OrderByDescending(x => x.Country == "ZA"));
                    list = new ObservableCollection<WeatherApp.Models.City.Welcome>(DeserializedObject.OrderByDescending(x => x.Country == "ZA"));
                }
            }
            catch(FileWriteException ex)
            {
                await pageDialogService.DisplayAlertAsync("Attention", ex.Message, "OK");
            }
            catch(FileReadException ex)
            {
                await pageDialogService.DisplayAlertAsync("Attention", ex.Message, "OK");
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
    }
}
