using Blazorise;
using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Components
{
	public partial class CropIndexCard
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        ICropRepository CropRepository { get; set; }

        [Parameter]
        public CropDto Crop { get; set; }
        [Parameter]
        public EventCallback<bool> onDeleteSuccess { get; set; }

        public string ImgSource { get; set; }

        private Modal modalRef;
       
        protected override void OnInitialized()
        {
            //todo make sure this uses a token to access the storage
            ImgSource = $"https://homeharveststorage.blob.core.windows.net/upload-container/{Crop.PlotImage}";
            base.OnInitialized();
        }
		private void ShowModal() => modalRef.Show();
		private void HideModal() => modalRef.Hide();
		private void NavigateToSown() => NavigationManager.NavigateTo($"Sown/{Crop.Id}");

		private async Task RemoveCrop()
        {     
            var result = await CropRepository.Delete(Crop.Id);
            if (!result)
            {
            //todo display problem on the modal over why deletion failed
                
            }
            else
            {
                HideModal();
                await onDeleteSuccess.InvokeAsync();   
            }
      
  }

		
  
    }
}