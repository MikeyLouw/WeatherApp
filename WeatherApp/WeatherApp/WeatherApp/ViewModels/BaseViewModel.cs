using System;
using System.Collections.Generic;
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

        public void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;

                OnPropertyChanged(PropertyName);
            }
        }
    }
}
