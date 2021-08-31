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
        RemoveItemModal DeleteSownModal { get; set; }
        EditSownModal EditModal { get; set; }
        public RemoveItem ItemToRemove { get; set; } = new();
        public SownDto EditItem = new ();
        public CropDto Crop { get; set; }
   
        /// <summary>
        /// Returns the calculated Harvest using PlantedOn and GrowInWeeks variables
        /// </summary>
         //	public DateTime HarvestDate => PlantedOn.AddDays(GrowInWeeks * 7.0);
        protected override async Task OnInitializedAsync()
        {
            Crop = new CropDto() { Sowed = new List<SownDto>()  };            
            await LoadData();
        }

        public async Task LoadData()
        {
            Crop = await CropRepository.GetCrop(int.Parse(id));    
            await InvokeAsync(StateHasChanged);     
        }
     

        private void DeleteSown(int id)
		{
            var item = Crop.Sowed.FirstOrDefault(i => id == i.Id);
            ItemToRemove.Id = item.Id;
            ItemToRemove.Description = $"{item.Plant.Name}, {item.PlantedOn.ToShortDateString()}";
            DeleteSownModal.RemoveModal.Show();
		}

        private async Task RemoveSown()
        {
            if (ItemToRemove.Delete)
            {         
                await SownRepository.Delete(ItemToRemove.Id);
                await LoadData();
            }
            ItemToRemove = new();
            DeleteSownModal.RemoveModal.Hide();
            
        }


        private async Task OpenEditModal(int id)
		{
            EditItem = Crop.Sowed.FirstOrDefault(i => i.Id == id);
            EditModal.EditSownItemModal.Show();
		}

        private async Task CloseEditModal(bool cancelled)
		{
            EditItem = new();
            EditModal.EditSownItemModal.Hide();
            if (!cancelled)
            {
                await LoadData();
            }
            }
    }
}