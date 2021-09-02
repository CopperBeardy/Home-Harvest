using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;
using HomeHarvest.Client.Components;
using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Client.Models;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace HomeHarvest.Client.Pages
{
    public partial class Sown
    {
        [Inject]
        public ISownRepository SownRepository { get; set; }
        [Inject]
        public ICropRepository CropRepository { get; set; }
        [Parameter]
        public string id { get; set; }
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
            Crop = await CropRepository.GetCrop(int.Parse(id));
            await InvokeAsync(StateHasChanged);
        }

        async void RemoveSown(SownDto sown)
        {
            var removeModal = Modal.Show<RemoveConfirmation>(
             $"Remove {sown.Plant.Name}, {sown.PlantedOn.ToShortDateString()}");
            var result = await removeModal.Result;
            if (!result.Cancelled)
            {
                await SownRepository.Delete(sown.Id);
            }
            await LoadData();
        }
        async void EditSownItem(SownDto sown)
        {
            var parameters = new ModalParameters();
            parameters.Add("Sown", sown);
            var editModal = Modal.Show<EditSown>("Edit",parameters);
            var result = await editModal.Result;
            if (!result.Cancelled)
            {
              

            }
            await LoadData();
        }
    }
        
}