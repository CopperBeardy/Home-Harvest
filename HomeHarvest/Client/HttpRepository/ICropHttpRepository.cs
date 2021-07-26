using HomeHarvest.Shared.Dtos;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeHarvest.Client.HttpRepository
{
	public interface ICropHttpRepository
	{
		Task<string> Create(CropDto crop);
		Task<string> Delete(CropDto crop);
		Task<string> DownloadPlotImage(int cropId);
		Task<string> Update(CropDto crop);
		Task<string> UploadPlotImage(MultipartFormDataContent content);
	}
}