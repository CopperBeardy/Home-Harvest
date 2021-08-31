using HomeHarvest.Shared.Dtos;
using System.Net.Http.Json;

namespace HomeHarvest.Client.HttpRepositories
{
	public class SownRepository : ISownRepository
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpClientFactory _factory;

		public SownRepository(IHttpClientFactory factory)
		{
			_factory = factory;
			_httpClient = _factory.CreateClient("HomeHarvest.ServerAPI");
		}
		public async Task<List<SownDto>> GetAll() =>
		await _httpClient.GetFromJsonAsync<List<SownDto>>($"api/Sown");
		public async Task<SownDto> Get(int id) =>
	await _httpClient.GetFromJsonAsync<SownDto>($"api/Sown/{id}");

		public async Task<bool> Create(CreateSownDto Sow) =>
			 (await _httpClient.PostAsJsonAsync("api/Sown", Sow)).IsSuccessStatusCode;

		public async Task<bool> Update(int id, SownDto Sow) =>
			(await _httpClient.PutAsJsonAsync($"api/Sown/{id}", Sow)).IsSuccessStatusCode;

		public async Task<bool> Delete(int id) =>
			(await _httpClient.DeleteAsync($"api/Sown/{id}")).IsSuccessStatusCode;


	}
}
