using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Components
{
    public partial class EditPlant
    {
        [Inject]
        PlantManager PlantManager { get; set; }
        [CascadingParameter]
        BlazoredModalInstance ModalInstance { get; set; }
        [Parameter]
        public PlantDto Plant { get; set; }
        async Task Cancel() => await ModalInstance.CancelAsync();

        public async Task HandleValidSubmit()
        {
            await PlantManager.Update(Plant);
            await ModalInstance.CloseAsync(ModalResult.Ok("success"));

        }
    }
}