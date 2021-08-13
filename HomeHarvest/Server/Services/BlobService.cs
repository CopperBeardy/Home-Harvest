using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HomeHarvest.Server.Models;
using Microsoft.Extensions.Options;

namespace HomeHarvest.Server.Services
{
	public class BlobService : IBlobService
	{
		private readonly string _azureConnectionString;
		private readonly string _azureContainerName;
		public BlobService(IOptions<BlobContainerConnection> conString)
		{
			_azureConnectionString = conString.Value.ConnectionString.ToString();
			_azureContainerName = "upload-container";
		}

		public async Task Upload(byte[] Image, string name)
		{
			var resizedImage = ImageConvertor.Sizer(Image);
			if (resizedImage.Length > 0)
			{
				var container = new BlobContainerClient(_azureConnectionString, _azureContainerName);
				await ContainerExist(container);
				var blob = container.GetBlobClient($"{name}");
				await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
				using (var fileStream = new MemoryStream(resizedImage))
				{
					await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = "image/png" });
				}
			}
		}
		private static async Task ContainerExist(BlobContainerClient container)
		{
			var createResponse = await container.CreateIfNotExistsAsync();
			if (createResponse != null && createResponse.GetRawResponse().Status == 201)
				await container.SetAccessPolicyAsync(PublicAccessType.Blob);
		}
	}
}