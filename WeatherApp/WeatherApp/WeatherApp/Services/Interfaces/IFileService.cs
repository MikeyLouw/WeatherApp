using System;
using System.IO;
namespace WeatherApp.Services.Interfaces
{
    public interface IFileService
    {
        void SaveFile(byte[] file, string name);
        string ReadFile(string name);
        void DeleteFile(string name);
    }
}
