using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;
using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Client.Pages;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Components
{
	public partial class EditSown
	{
		[Inject]
		private IPlantRepository PlantRepository { get; set; }
		[Inject]
		private ISownRepository SownRepository { get; set; }
		[CascadingParameter]
		BlazoredModalInstance ModalInstance { get; set; }
		[Parameter]
		public SownDto Sown { get; set; }
		public IEnumerable<PlantDto> Plants { get; set; } = new List<PlantDto>();

		protected override async Task OnInitializedAsync()
        {				
			 await LoadPlants();
			await InvokeAsync(StateHasChanged);
		}
		public async Task LoadPlants() =>Plants =  await PlantRepository.GetAll();
      

        void Cancel() => ModalInstance.CancelAsync();
		public async void SaveChanges()
		{
			//Todo validate data
			await SownRepository.Update(Sown.Id, Sown);
	
			await ModalInstance.CloseAsync(ModalResult.Ok("success"));
		}
	
	}
}