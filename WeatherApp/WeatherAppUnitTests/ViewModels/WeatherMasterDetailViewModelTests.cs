using Moq;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using WeatherApp.ViewModels;

namespace WeatherAppUnitTests.ViewModels
{
    [TestFixture]
    public class WeatherMasterDetailViewModelTests
    {
        private Mock<INavigationService> mockNavigationService;
        private Mock<IPageDialogService> mockPageDialogService;

        [SetUp]
        public void SetUp()
        {
            this.mockNavigationService = new Mock<INavigationService>();
            this.mockPageDialogService = new Mock<IPageDialogService>();
        }

        private WeatherMasterDetailViewModel CreateViewModel()
        {
            return new WeatherMasterDetailViewModel(
                this.mockNavigationService.Object,
                this.mockPageDialogService.Object);
        }

        [Test]
        public async Task Navigate_Error_ThrowsException_CallDialogService()
        {
            //Arrange
            mockNavigationService.Setup(x => x.NavigateAsync("Page")).ThrowsAsync(new System.Exception());
            mockPageDialogService.Setup(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            var ViewModel = this.CreateViewModel();
            //Act
            await ViewModel.Navigate("Page");
            //Assert
            mockPageDialogService.Verify(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task Navigate_Succes_Navigates()
        {
            //Arrange
            mockNavigationService.Setup(x => x.NavigateAsync("Page"));
            mockPageDialogService.Setup(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            var ViewModel = this.CreateViewModel();
            //Act
            await ViewModel.Navigate("Page");
            //Assert
            mockNavigationService.Verify(x => x.NavigateAsync(It.IsAny<string>()), Times.Once);
            mockPageDialogService.Verify(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
