
using HomeHarvest.Shared.Entities;
using System.Net.Http;

namespace HomeHarvest.Client.Services
{
    public class CropManager : APIManager<Crop>
    {
        public CropManager(HttpClient client) : base(client, "Crop")
        {
        }
        public static string DownloadPlotImage(string name) =>
            $"https://homeharveststorage.blob.core.windows.net/upload-container/{name}";
    }
}