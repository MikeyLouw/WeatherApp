using System;
using System.IO;
using System.Threading.Tasks;

namespace WeatherApp.Services.Interfaces
{
    public interface IFileService
    {
        void SaveFile(byte[] file, string name);
        Task<string> ReadFile(string name);
        void DeleteFile(string name);
        bool FileExists(string name);
    }
}
