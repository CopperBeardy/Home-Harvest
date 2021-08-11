using HomeHarvest.Shared;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components.Forms;
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

		public async Task<bool> UploadPlotImage(MultipartFormDataContent content) =>
			(await _httpClient.PostAsync("api/file", content)).IsSuccessStatusCode;

	


		public async Task<List<CropDto>> GetAll() => 
			await _httpClient.GetFromJsonAsync<List<CropDto>>("api/crop");
		
		public async Task<string> DownloadPlotImage(string name)=>
			$"https://homeharveststorage.blob.core.windows.net/upload-container/{name}";

		public async Task<bool> Create(CreateCropDto crop, MultipartFormDataContent content)
		{
			var uploadSuccess = false;
			var createSuccess = false;
			 await UploadPlotImage(content);
			createSuccess = (await _httpClient.PostAsJsonAsync("api/crop", crop)).IsSuccessStatusCode;
			
		
			return createSuccess;
		}

		public async Task<bool> Update(int id, CropDto crop)=>
			(await _httpClient.PutAsJsonAsync($"api/crop/{id}", crop)).IsSuccessStatusCode;

		public async Task<bool> Delete(int id) =>
			(await _httpClient.DeleteAsync($"api/crop/{id}")).IsSuccessStatusCode;


	}
}
