
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
	
		protected override async Task OnInitializedAsync()
		{
			await LoadData();
			await InvokeAsync(StateHasChanged);
		}
		
	
		public async Task LoadData()
		{
			CropItems = await CropRepository.GetAll();
			await InvokeAsync(StateHasChanged);
		}


		
	}
}