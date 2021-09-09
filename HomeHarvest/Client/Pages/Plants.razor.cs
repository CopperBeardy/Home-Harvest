using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Components;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Pages
{
    public partial class Plants
    {
        [Inject]
        PlantManager PlantManager { get; set; }
        [CascadingParameter]
        public IModalService Modal { get; set; }
        public IEnumerable<PlantDto> AllPlants { get; set; } = new List<PlantDto>();
    
        protected override async Task OnInitializedAsync() =>   await LoadData();
         
        public async Task LoadData()
        {
            AllPlants = await PlantManager.GetAll(); 
            await InvokeAsync(StateHasChanged);
        }

        public async void ShowAddPlantModal()
        {            
           var modalRef = Modal.Show<AddPlant>("Add Plant");
            var result = await modalRef.Result;
            if (!result.Cancelled)
            {
                AllPlants.Append(result.Data);
                await InvokeAsync(StateHasChanged);
            }
        }

        public async void ShowEditPlantModal(PlantDto plant)
        {      
            var parameters = new ModalParameters();
            parameters.Add("Plant", plant);
           var modalRef=  Modal.Show<EditPlant>("Edit plant", parameters);
            var result = await modalRef.Result;
            if (!result.Cancelled)
            {              
               await InvokeAsync(StateHasChanged);
            }            
        }
    }
}