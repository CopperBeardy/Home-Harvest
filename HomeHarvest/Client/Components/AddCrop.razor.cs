using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HomeHarvest.Client.Components
{
	public partial class AddCrop
	{
		public CreateCropDto Crop { get; set; } = new CreateCropDto();
		[Parameter]
		public bool ShowInputControls { get; set; } = false;
		[Parameter]
		public EventCallback<bool> onAdd { get; set; }

		[Inject]
		public ICropRepository CropRepository { get; set; }
		[Parameter]
		public MultipartFormDataContent Content { get; set; }
		public bool UploadSuccess { get; set; }

		public async Task HandleValidSubmit()
		{
			var success = await CropRepository.Create(Crop);
			if (success)
			{
				Crop = new CreateCropDto();
				ShowForm();
			}
			await onAdd.InvokeAsync(!success);
		}

		public void ShowForm() => ShowInputControls = !ShowInputControls;

		private async Task HandleSelected(InputFileChangeEventArgs e)
		{
			var imageFile = e.File;
			Crop.PlotImage = imageFile.Name;
			var stream = imageFile.OpenReadStream(imageFile.Size);
			MemoryStream ms = new();
			await stream.CopyToAsync(ms);
			Crop.Image = ms.ToArray();

			//         var imageFile = e.File;
			//       //  Crop.PlotImage = imageFile.Name;
			//using (var ms =  imageFile.OpenReadStream(imageFile.Size))
			//{
			//	var Content = new MultipartFormDataContent();
			//	 Content.Add(new StreamContent(ms, Convert.ToInt32(imageFile.Size)), "image", imageFile.Name);
			//}
		}
	}
}