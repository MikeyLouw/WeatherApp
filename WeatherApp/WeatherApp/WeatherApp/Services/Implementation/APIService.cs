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
using WeatherApp.Exceptions;
using WeatherApp.Exceptions.FileExceptions;

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
                var responseMessage = await this.httpClient.Get(await geoService.GetLocation());
                return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
            }
            catch (NotSupportedException ex)
            {
                throw new NotSupportedException("Not supported", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new JsonSerializationException("Request error, unable to retrieve the data.", ex);
            }
            catch (Exception ex)
            {
                throw new GeneralException(ex.Message, ex);
            }
        }

        public async Task<bool> GetLatestCities()
        {
            try
            {
                fileService.SaveFile(await httpClient.DownloadFile(), "City_List");
                return true;
            }
            catch (FileWriteException ex)
            {
                throw new FileWriteException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new GeneralException(ex.Message, ex);
            }
        }
    }
}
