using System;
using System.Collections.Generic;
using System.Net;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeatherApp.Models.City
{
    public partial class Welcome
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("coord")]
        public Coord Coord { get; set; }
    }

    public partial class Coord
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }
    }

    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter()
                {
                    DateTimeStyles = DateTimeStyles.AssumeUniversal,
                },
            },
        };
    }
}
