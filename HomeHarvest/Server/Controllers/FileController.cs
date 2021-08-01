using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HomeHarvest.Server.Helpers;
using HomeHarvest.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace HomeHarvest.Server.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _azureConnectionString;
        private readonly string _azureContainerName;
        public FileController(IOptions<BlobContainerConnection> conString )
        {
            _azureConnectionString = conString.Value.ConnectionString.ToString();
            _azureContainerName = "upload-container";
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
				var resizedImage = ImageConvertor.Sizer(file);
                if (resizedImage.Length > 0)
				{
					var container = new BlobContainerClient(_azureConnectionString, _azureContainerName);
					await ContainerExist(container);
					var blob = container.GetBlobClient(file.FileName);
					await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
					using (var fileStream = new MemoryStream(resizedImage))
					{
						await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = "image/png" });
					}
					return Ok(blob.Uri.ToString());
				}
				return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

		private static async Task ContainerExist(BlobContainerClient container)
		{
			var createResponse = await container.CreateIfNotExistsAsync();
			if (createResponse != null && createResponse.GetRawResponse().Status == 201)
				await container.SetAccessPolicyAsync(PublicAccessType.Blob);
		}

		//      [HttpGet("{name}")]
		//      public async Task<IActionResult> Download(string name)
		//{
		//          var token = new BlobSasBuilder()
		//          {
		//              BlobContainerName = _azureContainerName,
		//              BlobName = name,
		//              ExpiresOn = DateTime.UtcNow.AddMinutes(1)
		//          };
		//          token.SetPermissions(BlobAccountSasPermissions.Read);
		//          var stoken = token.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential("homeharveststorage", ));
		//          var container = new BlobContainerClient(_azureConnectionString, "upload-container");
		//          var client =   container.GetBlobClient(name);
		//          return Ok( client.Uri.ToString() + "?" + stoken);
		//      }

	}
}