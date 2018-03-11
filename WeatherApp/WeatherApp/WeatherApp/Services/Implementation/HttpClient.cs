using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Services.Interfaces;
using Plugin.Geolocator.Abstractions;
using System.IO;
using System.Net.Http.Headers;
using WeatherApp.Settings;
using WeatherApp.Exceptions;

namespace WeatherApp.Services.Implementation
{
    public class HttpClientService : IHttpClient
    {
        private static HttpClient _HttpClient = new HttpClient();

        public HttpClientService()
        {
        }

        public async Task<HttpResponseMessage> Get(Position location)
        {
            try
            {
                _HttpClient.BaseAddress = new Uri(Constants.URL_WEATHER + location.Latitude.ToString() + "&lon=" + location.Longitude.ToString() + "&APPID=" + Constants.KEY_OPENWEATHER);
                return await _HttpClient.GetAsync("");
            }
            catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException("Error while sending request.", ex);
            }
            catch (Exception ex)
            {
                throw new GeneralException(ex.Message, ex);
            }
        }

        public async Task<byte[]> DownloadFile()
        {
            try
            {
                _HttpClient.BaseAddress = new Uri(Constants.URL_CITIES);
                return await _HttpClient.GetByteArrayAsync("");
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Error while sending request.", ex);
            }
            catch (Exception ex)
            {
                throw new GeneralException(ex.Message, ex);
            }
        }
    }
}
