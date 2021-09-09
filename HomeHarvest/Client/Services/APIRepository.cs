using Newtonsoft.Json;
using System.Net.Http.Json;

namespace HomeHarvest.Client.Services
{
    public class APIRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpClientFactory _factory;
        readonly string _url;   

        public APIRepository(IHttpClientFactory factory,string controller)
		{
			_factory = factory;
			_httpClient = _factory.CreateClient("HomeHarvest.ServerAPI");
			_url = $"api/{controller}";
		}
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                var result = await _httpClient.GetAsync(_url);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(responseBody);
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TEntity> GetById(int id)
        {
            try
            {
                var result = await _httpClient.GetAsync($"{_url}/{id}");
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TEntity>(responseBody);
                return response;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            try
            {
				var result = await _httpClient.PostAsJsonAsync(_url, entity);
				result.EnsureSuccessStatusCode();
				string responseBody = await result.Content.ReadAsStringAsync();
				var response = JsonConvert.DeserializeObject<TEntity>(responseBody);
				return response;
            }
            catch (Exception)
            {
                //todo handle exception
              return null;
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync(_url, entity);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TEntity>(responseBody);    
                return response;
            }
            catch (Exception)
            {
             
                return null;
            }
         
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var result = await _httpClient.DeleteAsync ($"{_url}/{id}");
                result.EnsureSuccessStatusCode();         
          
                return true ;
            }
            catch (Exception)
            {
                return false;
            }          
        }
    }
}
