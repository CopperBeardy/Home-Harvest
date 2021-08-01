using HomeHarvest.Shared;
using HomeHarvest.Shared.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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

		public async Task<string> UploadPlotImage(MultipartFormDataContent content)
		{
			var postResult = await _httpClient.PostAsync("api/file", content);
			var postContent = await postResult.Content.ReadAsStringAsync();
			if (!postResult.IsSuccessStatusCode)
			{
				throw new ApplicationException(postContent);
			}
			else
			{
				return postContent;
			}
		}
		public async Task<List<CropDto>> GetAll()
		{
			var result = await _httpClient.GetFromJsonAsync<List<CropDto>>("api/crop");
		
			return result;
		}

		public async Task<string> DownloadPlotImage(string name)
		{

			return $"https://homeharveststorage.blob.core.windows.net/upload-container/{name}";
		}


		public async Task<bool> Create(CropDto crop)
		{
			
			var postResult =  await _httpClient.PostAsJsonAsync("api/crop", crop);
			return postResult.IsSuccessStatusCode;
		}

		public async Task<string> Update(CropDto crop)
		{
			//Todo Modify the crop data
			throw new NotImplementedException();
		}

		public async Task<string> Delete(CropDto crop)
		{
			//Todo delete crop and relate image and details
			throw new NotImplementedException();
		}

		
	}
}
