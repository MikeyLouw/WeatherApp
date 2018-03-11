using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Services.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml.Linq;
using System.IO;
using System.Text;

namespace WeatherApp.Services.Implementation
{
    public class APIService : IAPIService
    {
        private IGeoService geoService;
        private IFileService fileService;
        private IHttpClient httpClient;

        public APIService(IHttpClient httpClient, IGeoService geoService, IFileService fileService)
        {
            this.httpClient = httpClient;
            this.geoService = geoService;
            this.fileService = fileService;
        }

        public async Task<T> GetWeather<T>()
        {
            try
            {
                var location = await geoService.GetLocation();

                var responseMessage = await this.httpClient.Get(location);

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
                var response = await httpClient.Post();
                fileService.SaveFile(response, "City_List");
                var text = await fileService.ReadFile("City_List");

                var newtext = UTF8toASCII(text);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An Error occured", ex);
            }
        }

        public static string UTF8toASCII(string text)
        {
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            Byte[] encodedBytes = utf8.GetBytes(text);
            Byte[] convertedBytes =
                    Encoding.Convert(Encoding.UTF8, Encoding.ASCII, encodedBytes);
            System.Text.Encoding ascii = System.Text.Encoding.ASCII;

            return ascii.GetString(convertedBytes);
        }
    }
}
