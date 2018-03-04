using Prism.Navigation;
using Xamarin.Forms;

namespace WeatherApp.Views
{
    public partial class WeatherMasterDetailPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public WeatherMasterDetailPage()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation
        {
            get { return Device.Idiom != TargetIdiom.Phone; }
        }
    }
}
