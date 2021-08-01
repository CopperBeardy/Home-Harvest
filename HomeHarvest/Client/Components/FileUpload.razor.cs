using HomeHarvest.Client.HttpRepositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Components
{
	public partial class FileUpload
    {
        [Parameter]
        public string ImgUrl { get; set; }
        [Parameter]
        public EventCallback<string> OnChange { get; set; }
        [Inject]
        public ICropRepository Repository { get; set; }

		private async Task HandleSelected(InputFileChangeEventArgs e)
        {
            var imageFile = e.File;
			using (var ms = imageFile.OpenReadStream(imageFile.Size))
			{
				var content = new MultipartFormDataContent();	
				content.Add(new StreamContent(ms, Convert.ToInt32(imageFile.Size)), "image", imageFile.Name);
				ImgUrl = await Repository.UploadPlotImage(content);
			}
			await OnChange.InvokeAsync(imageFile.Name);
		}
    }
}