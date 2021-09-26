﻿using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Entities;
using Microsoft.AspNetCore.Components;
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
        public Sown Sown { get; set; }

        public IEnumerable<Plant> Plants { get; set; } = new List<Plant>();

        protected override async Task OnInitializedAsync()
        {
            await LoadPlants();
            await InvokeAsync(StateHasChanged);
        }
        public async Task LoadPlants() => Plants = await PlantManager.GetAll();

        void Cancel() => ModalInstance.CancelAsync();

        public void PlantValueChanged(int id) => Sown.Plant = Plants.FirstOrDefault(p => p.Id == id);

        public async Task SaveChanges()
        {
            await SownManager.Update(Sown);
            await ModalInstance.CloseAsync(ModalResult.Ok("success"));
        }
    }
}