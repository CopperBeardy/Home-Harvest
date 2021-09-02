using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;
using HomeHarvest.Client.Components;
using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Client.Models;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Pages
{
	public partial class Crops
	{
		[CascadingParameter]
		public IModalService Modal { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Inject]
		ICropRepository CropRepository { get; set; }
		IEnumerable<CropDto> CropItems { get; set; } = new List<CropDto>();
			
		protected override async Task OnInitializedAsync() => await LoadData();	
		
		void NavigateToSown(int id) => NavigationManager.NavigateTo($"Sown/{id}");
		async void RemoveCrop(int id)
		{			

			var result = await Modal.Show<RemoveConfirmation>(
                  $"Remove {CropItems.FirstOrDefault(c => c.Id == id).LocationYear}").Result;
			if (!result.Cancelled)
			{
				await CropRepository.Delete(id);
			}
			await LoadData();
		}
		public async Task LoadData()
		{
			CropItems = await CropRepository.GetAll();
			await InvokeAsync(StateHasChanged);
		}
	}
}