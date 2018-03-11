using Moq;
using NUnit.Framework;
using WeatherApp.Services.Implementation;

namespace WeatherAppUnitTests.Services.Implementation
{
    [TestFixture]
    public class GeoServiceTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        private GeoService CreateService()
        {
            return new GeoService();
        }
    }
}
