using Moq;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;
using WeatherApp.Services.Interfaces;
using WeatherApp.ViewModels;

namespace WeatherAppUnitTests.ViewModels
{
    [TestFixture]
    public class YourWeatherViewModelTests
    {
        private Mock<INavigationService> mockNavigationService;
        private Mock<IPageDialogService> mockPageDialogService;
        private Mock<IAPIService> mockAPIService;
        private Mock<IFileService> mockFileService;
        private Mock<IGeoService> mockGeoService;

        [SetUp]
        public void SetUp()
        {
            this.mockNavigationService = new Mock<INavigationService>();
            this.mockPageDialogService = new Mock<IPageDialogService>();
            this.mockAPIService = new Mock<IAPIService>();
            this.mockFileService = new Mock<IFileService>();
            this.mockGeoService = new Mock<IGeoService>();
        }

        private YourWeatherViewModel CreateViewModel()
        {
            return new YourWeatherViewModel(
                this.mockNavigationService.Object,
                this.mockPageDialogService.Object,
                this.mockAPIService.Object,
                this.mockFileService.Object,
                this.mockGeoService.Object);
        }

        [Test]
        public async Task LoadWeatherData_Success_NoNullFields()
        {
            //Arrange
            mockAPIService.Setup(x => x.GetWeather<WeatherApp.Models.Weather.Welcome>()).ReturnsAsync(new WeatherApp.Models.Weather.Welcome()
            {
                Weather = new WeatherApp.Models.Weather.Weather[1]
                {
                    new WeatherApp.Models.Weather.Weather()
                    {
                        Id = 1,
                        Icon = "icon"
                    }
                },             
                Main = new WeatherApp.Models.Weather.Main()
                {
                    TempMax = 10,
                    TempMin = 10
                },
                Name = "Name"
            });
            mockGeoService.Setup(x => x.GetCountryName()).ReturnsAsync("Country Name");
            var ViewModel = this.CreateViewModel();
            //Act
            await ViewModel.LoadWeatherData();
            //Assert
            Assert.NotNull(ViewModel.Date);
            Assert.NotNull(ViewModel.WeatherImage);
            Assert.NotNull(ViewModel.MaxTemp);
            Assert.NotNull(ViewModel.MinTemp);
            Assert.NotNull(ViewModel.Location);
        }

        [Test]
        public void LoadWeatherData_Error_CallsDialogService()
        {
            //Arrange
            mockPageDialogService.Setup(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            mockAPIService.Setup(x => x.GetWeather<WeatherApp.Models.Weather.Welcome>()).ThrowsAsync(new Exception());
            var ViewModel = this.CreateViewModel();
            //Act
            ViewModel.LoadWeatherData().Wait();
            //Assert
            mockPageDialogService.Verify(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        } 
    }
}
