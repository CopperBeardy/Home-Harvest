using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Components;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Pages
{
    public partial class Crops
	{
		[CascadingParameter]
		public IModalService Modal { get; set; }
		[Inject]
		NavigationManager NavigationManager { get; set; }
		[Inject]
        CropManager CropManager { get; set; }
		IEnumerable<CropDto> CropItems { get; set; } = new List<CropDto>();
			
		protected override async Task OnInitializedAsync() => await LoadData();	
	
		void NavigateToSown(int id) => NavigationManager.NavigateTo($"Sown/{id}");
		async void RemoveCrop(int id)
		{			
			var result = await Modal.Show<RemoveConfirmation>(
                  $"Remove {CropItems.FirstOrDefault(c => c.Id == id).LocationYear}").Result;
			if (!result.Cancelled)
			{
				await CropManager.Delete(id);
			}
			await LoadData();
		}
		public async Task LoadData()
		{
			CropItems = await CropManager.GetAll();		
			await InvokeAsync(StateHasChanged);
		}
	}
}