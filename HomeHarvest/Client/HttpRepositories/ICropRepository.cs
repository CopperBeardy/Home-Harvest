using HomeHarvest.Shared.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeHarvest.Client.HttpRepositories
{
	public interface ICropRepository
	{
		Task<List<CropDto>> GetAll();
		Task<bool> Create(CropDto crop);
		Task<string> Delete(CropDto crop);
		Task<string> DownloadPlotImage(int cropId);
		Task<string> Update(CropDto crop);
		Task<string> UploadPlotImage(MultipartFormDataContent content);
	}
}