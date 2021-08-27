
using Blazorise;
using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Pages
{
	public partial class Crops
	{
		[Inject]
		ICropRepository CropRepository { get; set; }
		IEnumerable<CropDto> CropItems { get; set; } = new List<CropDto>();
		private Modal RemoveModal;
		public string ToRemove;
		public int IdToRemove= -1;
		
		protected override async Task OnInitializedAsync()
		{
			await LoadData();
			await InvokeAsync(StateHasChanged);
		}
		
		public void DeleteCrop(int id)
		{
			IdToRemove = id;
			ToRemove = CropItems.FirstOrDefault(c => c.Id == id).LocationYear;
			RemoveModal.Show();			
		}
	
		private async Task RemoveCrop(bool remove)
		{
			if (remove)
			{
				var result = await CropRepository.Delete(IdToRemove);
				if (!result)
				{
					//todo display problem on the modal over why deletion failed

				}
				else
				{
					await LoadData();
				}
			}
			IdToRemove = -1;
			RemoveModal.Hide();
		}
		public async Task LoadData()
		{
			CropItems = await CropRepository.GetAll();
			await InvokeAsync(StateHasChanged);
		}


		
	}
}