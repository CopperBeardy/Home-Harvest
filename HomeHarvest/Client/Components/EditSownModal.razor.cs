using Blazorise;
using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Components
{
	public partial class EditSownModal
	{
		[Inject]
		private IPlantRepository PlantRepository { get; set; }
		[Inject]
		private ISownRepository SownRepository { get; set; }
	
		[Parameter]
		public Modal EditSownItemModal { get; set; }
		[Parameter]
		public SownDto SownItem { get; set; }
		[Parameter]
		public EventCallback<bool> CloseModal { get; set; }
		public IEnumerable<PlantDto> Plants { get; set; } = new List<PlantDto>();

		public int SelectedPlantId { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await	LoadPlants();
			SelectedPlantId = SownItem.Id;
		}
		public async Task LoadPlants() => Plants = await PlantRepository.GetAll();
		public async Task Cancel() =>
			await CloseModal.InvokeAsync(true);
		
		public async Task SaveChanges()
		{
			//Todo validate data
			
			SownItem.Plant = Plants.FirstOrDefault(p => p.Id == SelectedPlantId);
			await SownRepository.Update(SownItem.Id, SownItem);
			await CloseModal.InvokeAsync(true);
		}
	}
}