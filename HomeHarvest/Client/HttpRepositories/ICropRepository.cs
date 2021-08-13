using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Client.HttpRepositories
{
	public interface ICropRepository
	{
		Task<bool> Create(CreateCropDto crop);
		Task<bool> Delete(int id);
		Task<string> DownloadPlotImage(string name);
		Task<List<CropDto>> GetAll();
		Task<bool> Update(int id, CropDto crop);

	}
}