
using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Pages
{
	public partial class Crops
	{
		[Inject]
		ICropRepository CropRepository { get; set; }
		IEnumerable<CropDto> CropItems { get; set; }
		CropDto SelectedRow { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await LoadData();
			SetSelection();
		}
		async Task OnRowRemovingAsync(CropDto dataItem)
		{
			await CropRepository.Delete(dataItem.Id);
			await LoadData();
			StateHasChanged();
		}
		async Task OnRowUpdatingAsync(CropDto dataItem, IDictionary<string, object> newValues)
		{
			dataItem.Location = (string)newValues[nameof(CropDto.Location)];
			await CropRepository.Update(dataItem.Id, dataItem);
			await LoadData();
			StateHasChanged();
		}

		void SetSelection()
		{
			SelectedRow = CropItems.FirstOrDefault();
		}


		public async Task LoadData()
		{
			CropItems = await CropRepository.GetAll();
		}
	}
}