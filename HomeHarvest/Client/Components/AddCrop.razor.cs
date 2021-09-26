using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Components
{
    public partial class AddCrop
    {
        public CropDto Crop { get; set; } = new();
        [Parameter]
        public bool ShowInputControls { get; set; } = false;
        [Parameter]
        public EventCallback<bool> onAdd { get; set; }
        [Inject]
        CropManager? CropManager { get; set; }

        public async Task HandleValidSubmit()
        {
			 await CropManager.Insert(Crop);
            Crop = new CropDto();
            ShowForm();
            await onAdd.InvokeAsync();
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
        }
    }
}