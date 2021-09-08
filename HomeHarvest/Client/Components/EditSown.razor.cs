using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Components
{
	public partial class EditSown
	{
		[Inject]
		private PlantManager PlantManager { get; set; }
		[Inject]
		private SownManager SownManager { get; set; }
		[CascadingParameter]
		BlazoredModalInstance ModalInstance { get; set; }
		[Parameter]
		public SownDto Sown { get; set; }

		public int PlantId { get; set; }
		public DateTime PlantedOn { get; set; }
		public IEnumerable<PlantDto> Plants { get; set; } = new List<PlantDto>();

		protected override async Task OnInitializedAsync()
        {
			PlantId = Sown.PlantId;
			PlantedOn = Sown.PlantedOn;
			 await LoadPlants();
			await InvokeAsync(StateHasChanged);
		}
		public async Task LoadPlants() =>Plants =  await PlantManager.GetAll();


		void Cancel() => ModalInstance.CancelAsync();
	
		public async Task SaveChanges()
		{
			//Todo validate data
			Sown.Plant = Plants.FirstOrDefault(p  => p.Id == PlantId);
			Sown.PlantedOn = PlantedOn;
			await SownManager.Update(Sown);	
			await ModalInstance.CloseAsync(ModalResult.Ok("success"));
		}
	
	}
}