using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Components;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Pages
{
	public partial class Plants
	{
		[CascadingParameter]
		public IModalService Modal { get; set; }
		[Inject]
		PlantManager PlantManager { get; set; }
		IEnumerable<PlantDto> AllPlants { get; set; } = new List<PlantDto>();

		protected override async Task OnInitializedAsync() => await LoadData();

		public async Task LoadData()
		{
			AllPlants = await PlantManager.GetAll();
			await InvokeAsync(StateHasChanged);
		}

		public async void ShowAddPlantModal()
		{
			var result = await Modal.Show<AddPlant>("Add Plant").Result;
			if (!result.Cancelled)
			{
				await LoadData();
			}
		}

		public async void ShowEditPlantModal(PlantDto plant)
		{
			var parameters = new ModalParameters();
			parameters.Add("Plant", plant);
			var result = await Modal.Show<EditPlant>("Edit plant", parameters).Result;
			if (!result.Cancelled)
			{
				await LoadData();
			}
		}
	}
}