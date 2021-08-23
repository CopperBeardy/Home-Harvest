using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

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

        public CropDto Crop { get; set; }

        public string ImgSource { get; set; } = string.Empty;

        public SownDto SelectedRow { get; set; }



        /// <summary>
        /// Returns the calculated Harvest using PlantedOn and GrowInWeeks variables
        /// </summary>
         //	public DateTime HarvestDate => PlantedOn.AddDays(GrowInWeeks * 7.0);
        protected override async Task OnInitializedAsync()
        {
            Crop = new CropDto() { Sowed = new List<SownDto>()  };
            await LoadData();
            await InvokeAsync(StateHasChanged);
            //await FetchPlotImage();
        }

        public async Task FetchPlotImage()
        {
            throw new NotImplementedException();
        }

        public async Task AddNewSowedItem()
        {
            //todo:  move to separate component
            throw new NotImplementedException();
        }

        public async Task OnRowUpdatingAsync(SownDto dataItem, IDictionary<string, object> newValues)
        {
            dataItem = ParseValues(newValues);
            await SownRepository.Update(dataItem.Id, dataItem);
            throw new NotImplementedException();
        }

        public async Task OnRowRemovingAsync(SownDto dataItem)
        {
            await SownRepository.Delete(dataItem.Id);
            await LoadData();
            StateHasChanged();
        }

        public void PopulateImageWithSowedPins()
        {
            throw new NotImplementedException();
        }

        void SetSelection()
        {
            SelectedRow = Crop.Sowed.FirstOrDefault();
        }

        public async Task LoadData()
        {
         Crop = await CropRepository.GetCrop(int.Parse(id)); 
        }

        public SownDto ParseValues(IDictionary<string, object> values)
        {
            SownDto newItem = new();
            foreach (var item in values)
			{
				switch (item.Key)
				{
                    case "PlantedOn":
                        newItem.PlantedOn = DateTime.Parse(item.Value.ToString());
                        break;
                        
				}
			}
            return newItem;
        }
    }
}