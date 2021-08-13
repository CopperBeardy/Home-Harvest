using HomeHarvest.Shared.Dtos;
using System.Net.Http.Json;

namespace HomeHarvest.Client.HttpRepositories
{
	public class CropRepository : ICropRepository
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpClientFactory _factory;

		public CropRepository(IHttpClientFactory factory)
		{
			_factory = factory;
			_httpClient = _factory.CreateClient("HomeHarvest.ServerAPI");
		}
		public async Task<List<CropDto>> GetAll() =>
		await _httpClient.GetFromJsonAsync<List<CropDto>>("api/crop");

		public async Task<string> DownloadPlotImage(string name) =>
			$"https://homeharveststorage.blob.core.windows.net/upload-container/{name}";

		public async Task<bool> Create(CreateCropDto crop) =>
			 (await _httpClient.PostAsJsonAsync("api/crop", crop)).IsSuccessStatusCode;

		public async Task<bool> Update(int id, CropDto crop) =>
			(await _httpClient.PutAsJsonAsync($"api/crop/{id}", crop)).IsSuccessStatusCode;

		public async Task<bool> Delete(int id) =>
			(await _httpClient.DeleteAsync($"api/crop/{id}")).IsSuccessStatusCode;


	}
}
