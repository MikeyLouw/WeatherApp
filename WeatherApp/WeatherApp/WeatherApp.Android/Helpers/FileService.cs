using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WeatherApp.Exceptions.FileExceptions;
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
            try
            {
                var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(folderpath, string.Format("{0}.json.gz", name));
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                throw new FileDeleteException("Error while deleting a file.", ex);
            }
        }

        public async Task<string> ReadFile(string name)
        {
            try
            {
                var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(folderpath, string.Format("{0}.json.gz", name));

                if (!FileExists("temp", ".txt"))
                {
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
                }

                return File.ReadAllText(folderpath + "temp.txt");
            }
            catch (Exception ex)
            {
                throw new FileReadException("Unable to read a file.", ex);
            }
        }

        public void SaveFile(byte[] file, string name)
        {
            try
            {
                var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(folderpath, string.Format("{0}.json.gz", name));
                System.IO.File.WriteAllBytes(filePath, file);
            }
            catch (Exception ex)
            {
                throw new FileWriteException("Unable to save file. Storage could be full.", ex);
            }
        }

        public bool FileExists(string name, string extention){
            var folderpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderpath, string.Format("{0}{1}", new object[2] { name, extention }));
            return System.IO.File.Exists(filePath);
        }
    }
}
