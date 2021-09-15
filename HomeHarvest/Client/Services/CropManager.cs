using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Client.Services
{
    public class CropManager : APIRepository<CropDto>
    {
        public CropManager(IHttpClientFactory factory) : base(factory, "Crop")
        {
        }
        public static string DownloadPlotImage(string name) =>
            $"https://homeharveststorage.blob.core.windows.net/upload-container/{name}";
    }
}