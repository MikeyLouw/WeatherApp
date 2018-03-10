using System;
using System.Threading.Tasks;
using System.Net.Http;
using Plugin.Geolocator.Abstractions;
using System.IO;

namespace WeatherApp.Services.Interfaces
{
    public interface IHttpClient
    {
        Task<Stream> Post();
        Task<HttpResponseMessage> Get(Position location);
    }
}
