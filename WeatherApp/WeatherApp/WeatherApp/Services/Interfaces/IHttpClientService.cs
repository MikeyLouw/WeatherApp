using System;
using System.Threading.Tasks;
using System.Net.Http;
using Plugin.Geolocator.Abstractions;
using System.IO;

namespace WeatherApp.Services.Interfaces
{
    public interface IHttpClientService
    {
        Task<byte[]> DownloadFile();
        Task<HttpResponseMessage> Get(Position location);
    }
}
