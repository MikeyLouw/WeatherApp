using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WeatherApp.Exceptions
{
    public class GeneralException : Exception
    {
        public GeneralException(string message, Exception innerException) : base (message, innerException)
        {
            //var ErrorLogs = DependencyService.Get<IErrorLogService>();
            //var bytes = Encoding.ASCII.GetBytes(innerException.StackTrace);
            //ErrorLogs.Write(bytes);
        }
    }
}
