using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Pages;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Components
{
	public partial class AddSown
	{
		[Inject]
		PlantManager? PlantManager { get; set; }

		[Inject]
		SownManager? SownManager { get; set; }

		[CascadingParameter]
		BlazoredModalInstance? ModalInstance { get; set; }

		[Parameter]
		public Sown Sown { get; set; }

		public IEnumerable<Plant> Plants { get; set; } = new List<Plant>();
		protected override async Task OnInitializedAsync()
		{
			await LoadPlants();
			Sown.PlantId = Plants.FirstOrDefault().Id;
		
			await InvokeAsync(StateHasChanged);
		}

		public void PlantValueChanged(int id)
		{
			Sown.PlantId = id;
			
		}
		public async Task LoadPlants() => Plants = await PlantManager.GetAll();
		void Cancel() => ModalInstance.CancelAsync();
		public async Task HandleValidSubmit()
		{
			await SownManager.Insert(Sown);
			await ModalInstance.CloseAsync(ModalResult.Ok("success"));
		}
	}
}