using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeHarvest.Client.HttpRepositories
{
	public interface ICropRepository
	{
		Task<bool> Create(CreateCropDto crop, MultipartFormDataContent content);
		Task<bool> Delete(int id);
		Task<string> DownloadPlotImage(string name);
		Task<List<CropDto>> GetAll();
		Task<bool> Update(int id,CropDto crop);
		Task<bool> UploadPlotImage(MultipartFormDataContent content);
	}
}