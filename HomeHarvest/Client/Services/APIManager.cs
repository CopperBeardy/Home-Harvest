using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace HomeHarvest.Client.Services
{
    public class APIManager<TEntity> : IManager<TEntity> where TEntity : class
    {
        private readonly HttpClient _httpClient;
        readonly string _url;


        public APIManager(HttpClient client, string controller)
        {
            _httpClient = client;
            _url = $"api/{controller}";
            //URL = $"https://localhost:5003/{_url}";
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
            catch (Exception ex)
            {
                return null ;
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                var x = ex.Message;
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
                var result = await _httpClient.DeleteAsync($"{_url}/{id}");
                result.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
