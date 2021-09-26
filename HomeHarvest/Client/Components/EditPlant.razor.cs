using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Components
{
    public partial class EditPlant
    {
        [Inject]
        PlantManager PlantManager { get; set; }
        [CascadingParameter]
        BlazoredModalInstance ModalInstance { get; set; }
        [Parameter]
        public Plant Plant { get; set; }
        async Task Cancel() => await ModalInstance.CancelAsync();

        public async Task HandleValidSubmit()
        {
            await PlantManager.Update(Plant);
            await ModalInstance.CloseAsync(ModalResult.Ok("success"));

        }
    }
}