using Blazorise;
using HomeHarvest.Client.HttpRepositories;
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
        public SownDto CurrentItem{ get; set; }
        public CropDto Crop { get; set; }
   
        /// <summary>
        /// Returns the calculated Harvest using PlantedOn and GrowInWeeks variables
        /// </summary>
         //	public DateTime HarvestDate => PlantedOn.AddDays(GrowInWeeks * 7.0);
        protected override async Task OnInitializedAsync()
        {
            Crop = new CropDto() { Sowed = new List<SownDto>()  };
            
            await LoadData();
            await InvokeAsync(StateHasChanged);        
        }

        public async Task LoadData()
        {
             Crop = await CropRepository.GetCrop(int.Parse(id)); 
        }
        private Modal modalRef;
        private void ShowModal() => modalRef.Show();
        private void HideModal() => modalRef.Hide();

        private void ShowDeleteModal()
		{

		}

        private async Task RemovePlant()
        {
            var result = await SownRepository.Delete(1);
            if (!result)
            {
                //todo display problem on the modal over why deletion failed

            }
            else
            {
                HideModal();
                await LoadData();
            }
        }

    }
}