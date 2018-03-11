using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            var filePath = Path.Combine(folderpath, string.Format("{0}.json.gz", name));
            System.IO.File.Delete(filePath);
        }

        public async Task<string> ReadFile(string name)
        {
            var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderpath, string.Format("{0}.json.gz", name));

            using (FileStream fInStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (GZipStream zipStream = new GZipStream(fInStream, CompressionMode.Decompress))
                {
                    using (FileStream fOutStream = new FileStream(folderpath + "temp.txt", FileMode.Create, FileAccess.Write))
                    {
                        byte[] tempBytes = new byte[5000000];
                        int i;
                        while ((i = zipStream.Read(tempBytes, 0, tempBytes.Length)) != 0)
                        {
                            fOutStream.Write(tempBytes, 0, i);
                        }
                    }
                }
            }

            return File.ReadAllText(folderpath + "temp.txt");
        }

        public void SaveFile(byte[] file, string name)
        {
            var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderpath, string.Format("{0}.json.gz", name));
            System.IO.File.WriteAllBytes(filePath, file);
        }

        public bool FileExists(string name){
            var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderpath, string.Format("{0}.json.gz", name));
            return System.IO.File.Exists(filePath);
        }
    }
}
