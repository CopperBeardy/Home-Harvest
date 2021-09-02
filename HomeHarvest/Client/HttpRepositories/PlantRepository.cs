using HomeHarvest.Shared.Dtos;
using System.Net.Http.Json;

namespace HomeHarvest.Client.HttpRepositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _factory;

        public PlantRepository(IHttpClientFactory factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient("HomeHarvest.ServerAPI");
        }
        public async Task<List<PlantDto>> GetAll() =>
            
        await _httpClient.GetFromJsonAsync<List<PlantDto>>($"api/Plant");

        public async Task<bool> Create(CreatePlantDto plant) =>
             (await _httpClient.PostAsJsonAsync("api/Plant", plant)).IsSuccessStatusCode;

        public async Task<bool> Update(int id, PlantDto plant) =>
            (await _httpClient.PutAsJsonAsync($"api/Plant/{id}", plant)).IsSuccessStatusCode;

        public async Task<bool> Delete(int id) =>
            (await _httpClient.DeleteAsync($"api/Plant/{id}")).IsSuccessStatusCode;


    }
}
