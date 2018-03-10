using Moq;
using NUnit.Framework;
using WeatherApp.Services.Implementation;
using WeatherApp.Services.Interfaces;

namespace WeatherAppUnitTests.Services.Implementation
{
    [TestFixture]
    public class APIServiceTests
    {
        private MockRepository mockRepository;

        private Mock<IHttpClient> mockHttpClient;
        private Mock<IGeoService> mockGeoService;
        private Mock<IFileService> mockFileService;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockHttpClient = this.mockRepository.Create<IHttpClient>();
            this.mockGeoService = this.mockRepository.Create<IGeoService>();
            this.mockFileService = this.mockRepository.Create<IFileService>();
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
            APIService service = this.CreateService();


            // Assert

        }

        private APIService CreateService()
        {
            return new APIService(
                this.mockHttpClient.Object,
                this.mockGeoService.Object,
                this.mockFileService.Object);
        }
    }
}
