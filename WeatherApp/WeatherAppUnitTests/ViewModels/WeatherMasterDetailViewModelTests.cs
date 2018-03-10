using Moq;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using WeatherApp.ViewModels;

namespace WeatherAppUnitTests.ViewModels
{
    [TestFixture]
    public class WeatherMasterDetailViewModelTests
    {
        private MockRepository mockRepository;

        private Mock<INavigationService> mockNavigationService;
        private Mock<IPageDialogService> mockPageDialogService;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockNavigationService = this.mockRepository.Create<INavigationService>();
            this.mockPageDialogService = this.mockRepository.Create<IPageDialogService>();
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
            WeatherMasterDetailViewModel viewModel = this.CreateViewModel();


            // Assert

        }

        private WeatherMasterDetailViewModel CreateViewModel()
        {
            return new WeatherMasterDetailViewModel(
                this.mockNavigationService.Object,
                this.mockPageDialogService.Object);
        }
    }
}
