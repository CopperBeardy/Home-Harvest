using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Components
{
    public partial class AddSown
    {
        [Inject]
        PlantManager PlantManager { get; set; }

        [Inject]
        SownManager SownManager { get; set; }

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

        public void PlantValueChanged(int id) => Sown.PlantId = id;
        public async Task LoadPlants() => Plants = await PlantManager.GetAll();
        void Cancel() => ModalInstance.CancelAsync();
        public async Task HandleValidSubmit()
        {
            await SownManager.Insert(Sown);
            await ModalInstance.CloseAsync(ModalResult.Ok("success"));
        }
    }
}