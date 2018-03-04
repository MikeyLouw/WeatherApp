using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherApp.Services.Interfaces
{
    public interface IAPIService
    {
        Task<T> GetWeather<T>(T expectedResonse);
        Task<bool> GetLatestCities();
    }
}
