using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Exceptions.FileExceptions
{
    public class FileWriteException : Exception
    {
        public FileWriteException(string message, Exception innerException) : base (message, innerException)
        {

        }
    }

    public class FileReadException : Exception
    {
        public FileReadException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class FileDeleteException : Exception
    {
        public FileDeleteException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
