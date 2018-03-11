using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using WeatherApp.Exceptions.FileExceptions;
using WeatherApp.Services.Interfaces;
using WeatherApp.ViewModels;

namespace WeatherAppUnitTests.ViewModels
{
    [TestFixture]
    public class YourCitiesPageViewModelTests
    {
        private Mock<INavigationService> mockNavigationService;
        private Mock<IPageDialogService> mockPageDialogService;
        private Mock<IFileService> mockFileService;
        private Mock<IAPIService> mockAPIService;

        [SetUp]
        public void SetUp()
        {
            this.mockNavigationService = new Mock<INavigationService>();
            this.mockPageDialogService = new Mock<IPageDialogService>();
            this.mockFileService = new Mock<IFileService>();
            this.mockAPIService = new Mock<IAPIService>();
        }

        private YourCitiesPageViewModel CreateViewModel()
        {
            return new YourCitiesPageViewModel(
                this.mockNavigationService.Object,
                this.mockPageDialogService.Object,
                this.mockFileService.Object,
                this.mockAPIService.Object);
        }

        [Test]
        public void LoadCities_FileExists_Success_CitiesNotNull()
        {
            //Arrange
            mockFileService.Setup(x => x.FileExists("Name")).Returns(true);
            mockFileService.Setup(x => x.ReadFile("File")).ReturnsAsync(JsonConvert.SerializeObject(new WeatherApp.Models.City.Welcome()
            {
                Country = "South Africa",
                Id = 1,
                Name = "Name",
                Coord = new WeatherApp.Models.City.Coord() { Lat = 0, Lon = 0 }
            }));
            var ViewModel = this.CreateViewModel();
            //Act
            ViewModel.LoadCities().Wait();
            //Assert
            Assert.NotNull(ViewModel.Cities);
        }

        [Test]
        public void LoadCities_FileDontExcist_LoadCities()
        {
            //Arrange
            mockAPIService.Setup(x => x.GetLatestCities()).ReturnsAsync(true);
            mockFileService.Setup(x => x.ReadFile("File")).ReturnsAsync(JsonConvert.SerializeObject(new WeatherApp.Models.City.Welcome()
            {
                Coord = new WeatherApp.Models.City.Coord() { Lat = 0, Lon = 0 },
                Country = "South Africa",
                Id = 1,
                Name = "Name"
            }));
            var ViewModel = this.CreateViewModel();
            //Act
            ViewModel.LoadCities().Wait();
            //Assert
            Assert.NotNull(ViewModel.Cities);
        }

        [Test]
        public void LoadCities_Error_ThrowsReadFileException()
        {
            //Arrange
            mockFileService.Setup(x => x.FileExists("File")).Returns(true);
            mockFileService.Setup(x => x.ReadFile("File")).ThrowsAsync(new FileReadException("Error", new System.Exception()));
            mockPageDialogService.Setup(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            var ViewModel = this.CreateViewModel();
            //Act
            ViewModel.LoadCities().Wait();
            //Assert
            mockPageDialogService.Verify(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
