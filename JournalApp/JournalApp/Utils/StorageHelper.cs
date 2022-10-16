using PCLStorage;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JournalApp.Utils
{
    public class StorageHelper
    {
        public static async Task<string> DownloadPdF(string uri, string pdfPath)
        {

            try
            {
                HttpClient client = new HttpClient(HttpHelper.GetInsecureHandler());

                var response = client.GetAsync(uri).Result;


                IFolder folder = new FileSystemFolder(FileSystem.AppDataDirectory);
                var pdfName = Guid.NewGuid().ToString() + ".pdf";

                pdfPath = Path.Combine(folder.Path, pdfName);

                using (var fs = new FileStream(pdfPath, FileMode.CreateNew))
                {
                    await response.Content.CopyToAsync(fs);

                    IFile file = await folder.GetFileAsync(pdfName);
                    pdfPath = (new Uri(file.Path)).AbsoluteUri;
                }


                return pdfPath;

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:" + ex.Message);
            }

            return string.Empty;
        }
    }
}
