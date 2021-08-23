using HomeHarvest.Shared.Dtos;
using System.Net.Http.Json;

namespace HomeHarvest.Client.HttpRepositories
{
	public class CropRepository : ICropRepository
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpClientFactory _factory;
		private string _url = "api/crop";
		public CropRepository(IHttpClientFactory factory)
		{
			_factory = factory;
			_httpClient = _factory.CreateClient("HomeHarvest.ServerAPI");
		}
		public async Task<List<CropDto>> GetAll() =>
		await _httpClient.GetFromJsonAsync<List<CropDto>>(_url);

		public async Task<string> DownloadPlotImage(string name) =>
			$"https://homeharveststorage.blob.core.windows.net/upload-container/{name}";

		public async Task<bool> Create(CreateCropDto crop) =>
			 (await _httpClient.PostAsJsonAsync(_url, crop)).IsSuccessStatusCode;

		public async Task<bool> Update(int id, CropDto crop) =>
			(await _httpClient.PutAsJsonAsync($"{_url}/{id}", crop)).IsSuccessStatusCode;

		public async Task<bool> Delete(int id) =>
			(await _httpClient.DeleteAsync($"{_url}/{id}")).IsSuccessStatusCode;

		public async Task<CropDto> GetCrop(int cropId) =>
			await _httpClient.GetFromJsonAsync<CropDto>($"{_url}/{cropId}");
		
	

		
	}
}
