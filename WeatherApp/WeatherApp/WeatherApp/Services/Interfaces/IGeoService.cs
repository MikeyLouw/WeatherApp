using System;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace WeatherApp.Services.Interfaces
{
    public interface IGeoService
    {
        Task<Position> GetLocation();
        Task<string> GetCountryName();
    }
}
