using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Settings
{
    public static class Constants
    {
        //API KEYS
        public const string KEY_OPENWEATHER = "fbf2a0cc41b369f34bb58e2fc36c2199";
        //API ENDPOINTS
        public const string URL_WEATHER = "http://api.openweathermap.org/data/2.5/weather?units=metric&lat=";
        public const string URL_CITIES = "http://bulk.openweathermap.org/sample/city.list.json.gz";
        public const string URL_WEATHERICON = "http://openweathermap.org/img/w/";
    }
}
