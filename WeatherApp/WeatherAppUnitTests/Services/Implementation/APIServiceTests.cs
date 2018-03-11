using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
using WeatherApp.Exceptions;
using WeatherApp.Exceptions.FileExceptions;
using WeatherApp.Services.Implementation;
using WeatherApp.Services.Interfaces;

namespace WeatherAppUnitTests.Services.Implementation
{
    [TestFixture]
    public class APIServiceTests
    {
        private Mock<IHttpClientService> mockHttpClient;
        private Mock<IGeoService> mockGeoService;
        private Mock<IFileService> mockFileService;

        [SetUp]
        public void SetUp()
        {
            this.mockHttpClient = new Mock<IHttpClientService>();
            this.mockGeoService = new Mock<IGeoService>();
            this.mockFileService = new Mock<IFileService>();
        }

        private APIService CreateService()
        {
            return new APIService(
                this.mockHttpClient.Object,
                this.mockGeoService.Object,
                this.mockFileService.Object);
        }

        [Test]
        public void GetWeather_Success_ReturnsObjectOfTypeWelcome()
        {
            //Arrange
            mockGeoService.Setup(x => x.GetLocation()).ReturnsAsync(new Plugin.Geolocator.Abstractions.Position() { Latitude = 0, Longitude = 0 });
            mockHttpClient.Setup(x => x.Get(new Plugin.Geolocator.Abstractions.Position())).ReturnsAsync(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new WeatherApp.Models.Weather.Welcome()
                {
                    Coord = new WeatherApp.Models.Weather.Coord() { Lat = 0, Lon = 0 },
                    Clouds = new WeatherApp.Models.Weather.Clouds() { All = 50 },
                    Visibility = 10
                }))
            });
            var Service = this.CreateService();
            //Act
            var res = Service.GetWeather<WeatherApp.Models.Weather.Welcome>().Result;
            //Assert
            Assert.NotNull(res);
        }

        [Test]
        public void GetWeather_Error_ThrowsNotSupportedException()
        {
            //Arrange
            mockGeoService.Setup(x => x.GetLocation()).ThrowsAsync(new NotSupportedException());
            var Service = this.CreateService();
            //Act //Assert
            Assert.Catch<NotSupportedException>(async () => { await Service.GetWeather<WeatherApp.Models.Weather.Welcome>(); });
        }

        [Test]
        public void GetWeather_Error_ThrowsJsonSerializationException()
        {
            //Arrange
            mockGeoService.Setup(x => x.GetLocation()).ReturnsAsync(new Plugin.Geolocator.Abstractions.Position() { Latitude = 0, Longitude = 0 });
            mockHttpClient.Setup(x => x.Get(new Plugin.Geolocator.Abstractions.Position())).ReturnsAsync(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("Some deserializable data that will fail")
            });
            var Service = this.CreateService();
            //Act //Assert
            Assert.Catch<JsonSerializationException>(async () => { await Service.GetWeather<WeatherApp.Models.Weather.Welcome>(); });
        }

        [Test]
        public void GetWeather_Error_ThrowsGeneralException()
        {
            //Arrange
            mockGeoService.Setup(x => x.GetLocation()).ReturnsAsync(new Plugin.Geolocator.Abstractions.Position() { Latitude = 0, Longitude = 0 });
            mockHttpClient.Setup(x => x.Get(new Plugin.Geolocator.Abstractions.Position())).ThrowsAsync(new GeneralException("error", new Exception()));
            var Service = this.CreateService();
            //Act //Assert
            Assert.Catch<GeneralException>(async () => { await Service.GetWeather<WeatherApp.Models.Weather.Welcome>(); });
        }

        [Test]
        public void GetLatestCities_Success_ReturnTrue()
        {
            //Arrange
            mockHttpClient.Setup(x => x.DownloadFile()).ReturnsAsync(new byte[1000]);
            mockFileService.Setup(x => x.SaveFile(new byte[1000], "Name"));
            var Service = this.CreateService();
            //Act
            var res = Service.GetLatestCities().Result;
            //Assert
            Assert.IsTrue(res);
        }

        [Test]
        public void GetLatestCities_Error_ThrowsFileWriteException()
        {
            //Arrange
            mockHttpClient.Setup(x => x.DownloadFile()).ReturnsAsync(new byte[1000]);
            mockFileService.Setup(x => x.SaveFile(new byte[1000], "Name")).Throws(new FileWriteException("Message", new Exception()));
            var Service = this.CreateService();
            //Act //Assert
            Assert.Catch<FileWriteException>(async () => { await Service.GetLatestCities(); });
        }

        [Test]
        public void GetLatestCities_HttpError_ThrowsGeneralException()
        {
            //Arrange
            mockHttpClient.Setup(x => x.DownloadFile()).ThrowsAsync(new Exception());
            mockFileService.Setup(x => x.SaveFile(new byte[1000], "Name"));
            var Service = this.CreateService();
            //Act //Assert
            Assert.Catch<GeneralException>(async () => { await Service.GetLatestCities(); });
        }
    }
}
