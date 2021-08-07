using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Pages
{
	public partial class AddCrop
    {        
        [Inject]
        IHttpClientFactory _httpClientFactory { get; set; }
        public CreateCropDto Crop { get; set; } = new();
        [Parameter]
        public bool isVisible { get; set; }

        [Parameter]
        public EventCallback<bool> onAdd { get; set; }

        [Inject]
        public ICropRepository CropRepository { get; set; }

        public bool UploadSuccess { get; set; }

        public async Task HandleValidSubmit()
        {
            var success = false;
            if (UploadSuccess)
            {
                    success = await CropRepository.Create(Crop);
            } 
            await onAdd.InvokeAsync(!success);
        }

        private async Task HandleSelected(InputFileChangeEventArgs e)
        {
            var imageFile = e.File;  
            Crop.PlotImage = imageFile.Name;
            using (var ms = imageFile.OpenReadStream(imageFile.Size))
            {
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(ms, Convert.ToInt32(imageFile.Size)), "image", imageFile.Name);
                UploadSuccess = await CropRepository.UploadPlotImage(content);
            }
        }
    }
}