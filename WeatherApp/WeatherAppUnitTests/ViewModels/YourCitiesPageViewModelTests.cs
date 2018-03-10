using Moq;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using WeatherApp.Services.Interfaces;
using WeatherApp.ViewModels;

namespace WeatherAppUnitTests.ViewModels
{
    [TestFixture]
    public class YourCitiesPageViewModelTests
    {
        private MockRepository mockRepository;

        private Mock<INavigationService> mockNavigationService;
        private Mock<IPageDialogService> mockPageDialogService;
        private Mock<IFileService> mockFileService;
        private Mock<IAPIService> mockAPIService;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockNavigationService = this.mockRepository.Create<INavigationService>();
            this.mockPageDialogService = this.mockRepository.Create<IPageDialogService>();
            this.mockFileService = this.mockRepository.Create<IFileService>();
            this.mockAPIService = this.mockRepository.Create<IAPIService>();
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
            YourCitiesPageViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private YourCitiesPageViewModel CreateViewModel()
        {
            return new YourCitiesPageViewModel(
                this.mockNavigationService.Object,
                this.mockPageDialogService.Object,
                this.mockFileService.Object,
                this.mockAPIService.Object);
        }
    }
}
