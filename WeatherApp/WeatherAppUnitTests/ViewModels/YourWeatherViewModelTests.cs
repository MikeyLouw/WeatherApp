using Moq;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using WeatherApp.Services.Interfaces;
using WeatherApp.ViewModels;

namespace WeatherAppUnitTests.ViewModels
{
    [TestFixture]
    public class YourWeatherViewModelTests
    {
        private MockRepository mockRepository;

        private Mock<INavigationService> mockNavigationService;
        private Mock<IPageDialogService> mockPageDialogService;
        private Mock<IAPIService> mockAPIService;
        private Mock<IFileService> mockFileService;
        private Mock<IGeoService> mockGeoService;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockNavigationService = this.mockRepository.Create<INavigationService>();
            this.mockPageDialogService = this.mockRepository.Create<IPageDialogService>();
            this.mockAPIService = this.mockRepository.Create<IAPIService>();
            this.mockFileService = this.mockRepository.Create<IFileService>();
            this.mockGeoService = this.mockRepository.Create<IGeoService>();
        }

        [TearDown]
        public void TearDown()
        {
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void TestMethod1()
        {
            // Arrange


            // Act
            YourWeatherViewModel viewModel = this.CreateViewModel();


            // Assert

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
    }
}
