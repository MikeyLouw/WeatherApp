using Xamarin.Forms;

namespace WeatherApp.Views
{
    public partial class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage()
        {
            InitializeComponent();

            this.BarBackgroundColor = Color.Gray;
            this.BarTextColor = Color.White;
        }
    }
}
