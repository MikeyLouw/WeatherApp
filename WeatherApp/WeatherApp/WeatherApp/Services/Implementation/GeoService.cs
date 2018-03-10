﻿using System;
using System.Threading.Tasks;
using WeatherApp.Services.Interfaces;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Linq;


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
                if (CrossGeolocator.IsSupported){
                    var a = CrossGeolocator.Current.IsGeolocationEnabled;
                    var b = CrossGeolocator.Current.IsGeolocationAvailable;
                    var position = await CrossGeolocator.Current.GetPositionAsync();
                    var locationDetails = await CrossGeolocator.Current.GetAddressesForPositionAsync(position);
                    return locationDetails.FirstOrDefault().CountryName;
                }
                else{
                    throw new NotSupportedException();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured", ex);
            }
        }

        public async Task<Position> GetLocation()
        {
            if (CrossGeolocator.IsSupported){
                var res = await CrossGeolocator.Current.GetPositionAsync();
                return res;
            }
            else{
                throw new NotSupportedException();
            }
        }
    }
}
