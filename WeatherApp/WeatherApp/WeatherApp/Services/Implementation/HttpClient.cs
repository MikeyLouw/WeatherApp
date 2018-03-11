using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Services.Interfaces;
using Plugin.Geolocator.Abstractions;
using System.IO;
using System.Net.Http.Headers;

namespace WeatherApp.Services.Implementation
{
    public class HttpClientService : IHttpClient
    {
        public static HttpClient _HttpClient;

        public HttpClientService()
        {
            _HttpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> Get(Position location)
        {
            try
            {
                _HttpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?units=metric&lat=" + location.Latitude.ToString() + "&lon=" + location.Longitude.ToString() + "&APPID=fbf2a0cc41b369f34bb58e2fc36c2199");
                var responseMessage = await _HttpClient.GetAsync("");
                return responseMessage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<byte[]> Post()
        {
            try
            {
                _HttpClient.BaseAddress = new Uri("http://bulk.openweathermap.org/sample/city.list.json.gz");
                _HttpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/octet-stream"));
                var response = await _HttpClient.GetByteArrayAsync("");
                _HttpClient.DefaultRequestHeaders.Clear();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
