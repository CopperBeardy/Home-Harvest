using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Components;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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
      
        public CropDto Crop { get; set; }
        public string imgsrc { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Crop = new CropDto() { Sowed = new List<SownDto>() };

            await LoadData();
         
        }

        public async Task AddPOI(MouseEventArgs args)
        {
            // use offsets as location of click on the image to
            // drop a pin
            var sown = new SownDto()
            {
                CropId = Crop.Id,
                PoiX = int.Parse(args.OffsetX.ToString()),
                PoiY = int.Parse(args.OffsetY.ToString()),
                PlantedOn = DateTime.Today,
                PlantId = 0,
              };
            var parameters = new ModalParameters();
            parameters.Add("Sown",sown);
            var response =Modal.Show<AddSown>("New Sown",parameters);
            var result = await response.Result;
            if (!result.Cancelled)
            {
                await LoadData();
            }
        }

        public async Task LoadData()
        {
            Crop = await CropManager.GetById(int.Parse(Id));
            imgsrc = $"https://homeharveststorage.blob.core.windows.net/upload-container/{Crop.PlotImage}";
          // add POI pins to img
               await InvokeAsync(StateHasChanged);
    
        }

        async Task RemoveSown(SownDto sown)
        {
            var removeModal = Modal.Show<RemoveConfirmation>(
             $"Remove {sown.Plant.Name}, {sown.PlantedOn.ToShortDateString()}");
            var result = await removeModal.Result;
            if (!result.Cancelled)
            {
                await SownManager.Delete(sown.Id);
            }
            await LoadData();

        }
        async Task EditSownItem(SownDto sown)
        {
           var parameters = new ModalParameters();
            parameters.Add("Sown", sown);
             var resposne = Modal.Show<EditSown>("Edit",parameters);          
            var result = await resposne.Result;
            if(!result.Cancelled)
            {
                await LoadData();
            }
        }
    }        
}