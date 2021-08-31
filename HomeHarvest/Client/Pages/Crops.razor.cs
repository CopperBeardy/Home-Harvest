using Blazorise;
using HomeHarvest.Client.Components;
using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Client.Models;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Pages
{
	public partial class Crops
	{
		[Inject]
		ICropRepository CropRepository { get; set; }
		IEnumerable<CropDto> CropItems { get; set; } = new List<CropDto>();
		 RemoveItemModal DeleteCropModal { get; set; }
		public RemoveItem CurrentItem { get; set; } = new();
			
		protected override async Task OnInitializedAsync()
		{
			await LoadData();	
		}

		/// <summary>
		/// Open the modal to confirm request to delete items
		/// </summary>
		/// <param name=""="id"></param>
		public void DeleteCrop(int id)
		{
			CurrentItem.Id = id;
			CurrentItem.Description = CropItems.FirstOrDefault(c => c.Id == id).LocationYear;
			 DeleteCropModal.RemoveModal.Show();		
		}
	
		private async Task RemoveCrop()
		{
			if (CurrentItem.Delete)
			{
				await CropRepository.Delete(CurrentItem.Id);
				await LoadData();			
			}
			CurrentItem = new();
			DeleteCropModal.RemoveModal.Hide();
		}

		public async Task LoadData()
		{
			CropItems = await CropRepository.GetAll();
			await InvokeAsync(StateHasChanged);
		}
	}
}