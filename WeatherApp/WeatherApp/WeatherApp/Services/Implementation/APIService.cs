using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Services.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;

namespace WeatherApp.Services.Implementation
{
    public class APIService : IAPIService
    {
        public IGeoService geoService;
        public IFileService fileService;
        public static HttpClient HttpClient;

        public APIService(IGeoService geoService, IFileService fileService)
        {
            HttpClient = new HttpClient(new HttpClientHandler(), true);
            HttpClient.Timeout = TimeSpan.FromSeconds(30);
#if DEBUG
            HttpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?");
#else
            HttpClient.BaseAddress = new Uri("");
#endif
            this.geoService = geoService;
            this.fileService = fileService;
        }

        public async Task<T> GetWeather<T>(T expectedResonse)
        {
            try
            {
                var location = await geoService.GetLocation();
                var responseMessage = await HttpClient.GetAsync(string.Format("lat={0}&lon={1}", new string[2]{location.Latitude.ToString(), location.Longitude.ToString()}));
                var contentBody = await responseMessage.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<T>(contentBody);
                return responseObject;
            }
            catch (NotSupportedException ex)
            {
                throw new Exception("Location services not supported on your device.", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new Exception("No data was returned", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An Error occured", ex);
            }
        }

        public async Task<bool> GetLatestCities()
        {
            try
            {
                HttpClient.BaseAddress = new Uri("http://bulk.openweathermap.org/sample/city.list.json.gz");
                var response = await HttpClient.GetByteArrayAsync("");
                fileService.SaveFile(response, "City_List");
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An Error occured", ex);
            }
        }
    }
}
