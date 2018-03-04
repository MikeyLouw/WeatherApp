using System;
using System.Threading.Tasks;
using WeatherApp.Services.Interfaces;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;


namespace WeatherApp.Services.Implementation
{
    public class GeoService : IGeoService
    {
        public GeoService()
        {
        }

        public async Task<Position> GetLocation()
        {
            if (CrossGeolocator.IsSupported){
                return await CrossGeolocator.Current.GetPositionAsync();
            }
            else{
                throw new NotSupportedException();
            }
        }
    }
}
