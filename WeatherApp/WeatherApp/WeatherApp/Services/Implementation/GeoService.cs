using System;
using System.Threading.Tasks;
using WeatherApp.Services.Interfaces;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Linq;
using WeatherApp.Exceptions;

namespace WeatherApp.Services.Implementation
{
    public class GeoService : IGeoService
    {
        public GeoService()
        {
        }

        public async Task<string> GetCountryName()
        {
            try
            {
                if (CrossGeolocator.IsSupported) {
                    var locationDetails = await CrossGeolocator.Current.GetAddressesForPositionAsync(await CrossGeolocator.Current.GetPositionAsync());
                    return locationDetails.FirstOrDefault().CountryName;
                }
                else{
                    throw new NotSupportedException();
                }
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException("No data was returned", ex);
            }
            catch (Exception ex)
            {
                throw new GeneralException(ex.Message, ex);
            }
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
