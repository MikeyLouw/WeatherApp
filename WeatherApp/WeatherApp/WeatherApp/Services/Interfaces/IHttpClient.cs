using System;
using System.Threading.Tasks;
using System.Net.Http;
using Plugin.Geolocator.Abstractions;
namespace WeatherApp.Services.Interfaces
{
    public interface IHttpClient
    {
        Task<Byte[]> Post();
        Task<HttpResponseMessage> Get(Position location);
    }
}
