using Moq;
using NUnit.Framework;
using WeatherApp.Services.Implementation;

namespace WeatherAppUnitTests.Services.Implementation
{
    [TestFixture]
    public class GeoServiceTests
    {
        private MockRepository mockRepository;



        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [TearDown]
        public void TearDown()
        {
            this.mockRepository.VerifyAll();
        }

        private GeoService CreateService()
        {
            return new GeoService();
        }
    }
}
