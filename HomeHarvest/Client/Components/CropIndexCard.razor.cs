using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Components
{
	public partial class CropIndexCard
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Parameter]
        public CropDto Crop { get; set; }
        [Parameter]    
        public EventCallback<int> DeleteCrop { get; set; }
        public string ImgSource { get; set; }
        protected override void OnInitialized()
        {
            //todo make sure this uses a token to access the storage
            ImgSource = $"https://homeharveststorage.blob.core.windows.net/upload-container/{Crop.PlotImage}";
            base.OnInitialized();
        }

		private void NavigateToSown() => 
            NavigationManager.NavigateTo($"Sown/{Crop.Id}");

		private async Task RemoveCrop() => 
            await DeleteCrop.InvokeAsync(Crop.Id);   
    }
}