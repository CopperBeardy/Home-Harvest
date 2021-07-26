using HomeHarvest.Shared.Dtos;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeHarvest.Client.HttpRepository
{
	public class CropHttpRepository : ICropHttpRepository
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpClientFactory _factory;

		public CropHttpRepository(IHttpClientFactory factory)
		{
			_factory = factory;
			_httpClient = _factory.CreateClient("HomeHarvest.ServerAPI");
		}

		public async Task<string> UploadPlotImage(MultipartFormDataContent content)
		{
			var postResult = await _httpClient.PostAsync("api/upload", content);
			var postContent = await postResult.Content.ReadAsStringAsync();
			if (!postResult.IsSuccessStatusCode)
			{
				throw new ApplicationException(postContent);
			}
			else
			{
				var imgUrl = Path.Combine("https://localhost:5003", postContent);
				return imgUrl;
			}
		}

		public async Task<string> DownloadPlotImage(int cropId)
		{
			//Todo return image from the hosted server
			throw new NotImplementedException();
		}

		public async Task<string> Create(CropDto crop)
		{
			//todo create a new crop object
			throw new NotImplementedException();
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
