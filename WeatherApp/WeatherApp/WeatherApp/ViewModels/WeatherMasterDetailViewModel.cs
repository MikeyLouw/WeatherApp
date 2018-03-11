using System;
using Prism.Navigation;
using Prism.Services;
using Prism.Commands;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace WeatherApp.ViewModels
{
    public class WeatherMasterDetailViewModel : BaseViewModel, INavigationAware
    {
        public Command<string> NavigateCommand { get; private set; }

        public WeatherMasterDetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService): 
        base(navigationService, pageDialogService)
        {
            NavigateCommand = new Command<string>(async (param) => { await Navigate(param); });
        }

        public async Task Navigate(string url){
            try
            {
                await this.navigationService.NavigateAsync(url);
            }
            catch(Exception ex)
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
