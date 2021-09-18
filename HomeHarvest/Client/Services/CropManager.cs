using HomeHarvest.Shared.Dtos;
using System.Net.Http;

namespace HomeHarvest.Client.Services
{
    public class CropManager : APIRepository<CropDto>
    {
        public CropManager(HttpClient client) : base(client, "Crop")
        {
        }
        public static string DownloadPlotImage(string name) =>
            $"http://127.0.0.1:10000/upload-container/{name}";
    }
}