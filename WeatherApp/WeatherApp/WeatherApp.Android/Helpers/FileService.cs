using System;
using System.IO;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Droid.Helpers
{
    public class FileService : IFileService
    {
        public FileService()
        {
        }

        public void DeleteFile(string name)
        {
            var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderpath, string.Format("{0}.json", name));
            System.IO.File.Delete(filePath);
        }

        public string ReadFile(string name)
        {
            var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderpath, string.Format("{0}.json", name));
            return System.IO.File.OpenText(filePath).ReadToEnd();
        }

        public void SaveFile(byte[] file, string name)
        {
            var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderpath, string.Format("{0}.json", name));
            System.IO.File.WriteAllBytes(filePath, file);
        }
    }
}
