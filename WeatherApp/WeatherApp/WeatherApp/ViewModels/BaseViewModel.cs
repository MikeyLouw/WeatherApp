using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
namespace WeatherApp.ViewModels
{
    public class BaseViewModel : BindableBase, INotifyPropertyChanged
    {
        protected INavigationService navigationService;
        protected IPageDialogService pageDialogService;

        public BaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
