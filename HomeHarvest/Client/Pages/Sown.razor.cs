using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Components;
using HomeHarvest.Client.Models;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Pages
{
    public partial class Sown
    {
        
      [Inject]
        CropManager CropManager { get; set; }
        [Inject]
        SownManager SownManager { get; set; }
        [Parameter]
        public string Id { get; set; }
        [CascadingParameter]
        public IModalService Modal { get; set; }

        public SownDto EditItem = new();
        public CropDto Crop { get; set; }

        /// <summary>
        /// Returns the calculated Harvest using PlantedOn and GrowInWeeks variables
        /// </summary>

        protected override async Task OnInitializedAsync()
        {
            Crop = new CropDto() { Sowed = new List<SownDto>() };
            await LoadData();
        }

        public async Task LoadData()
        {
            Crop = await CropManager.GetById(int.Parse(Id));
            await InvokeAsync(StateHasChanged);
        }

        async void RemoveSown(SownDto sown)
        {
            var options = new ModalOptions()
            {
                HideCloseButton = true
            };
            var removeModal = Modal.Show<RemoveConfirmation>(
             $"Remove {sown.Plant.Name}, {sown.PlantedOn.ToShortDateString()}",options);
            var result = await removeModal.Result;
            if (!result.Cancelled)
            {
                await SownManager.Delete(sown.Id);
            }
            await LoadData();
        }
        async void EditSownItem(SownDto sown)
        {
            var options = new ModalOptions()
            {
                HideCloseButton =true
            };
            var parameters = new ModalParameters();
            parameters.Add("Sown", sown);
            var editModal = Modal.Show<EditSown>("Edit",parameters,options);
            await editModal.Result;
       
            await LoadData();
        }
    }
        
}