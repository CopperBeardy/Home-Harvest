using Blazored.Modal;
using Blazored.Modal.Services;
using HomeHarvest.Client.Components;
using HomeHarvest.Client.Services;
using HomeHarvest.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace HomeHarvest.Client.Pages
{
    public partial class Sows
    {
        [Inject]
        CropManager CropManager { get; set; }
        [Inject]
        SownManager SownManager { get; set; }
        [Parameter]
        public string Id { get; set; }
        [CascadingParameter]
        public IModalService Modal { get; set; }

        public Crop Crop { get; set; }
        public string ImgSrc { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Crop = new Crop() { Sowed = new List<Sown>() };
            await LoadData();
        }

        public async Task AddSown(MouseEventArgs args)
        {
            // use offsets as location of click on the image to
            // drop a pin
            var parameters = new ModalParameters();
            parameters.Add("Sown", new Sown()
            {
                CropId = Crop.Id,
                PoiX = int.Parse(args.OffsetX.ToString()),
                PoiY = int.Parse(args.OffsetY.ToString()),
                PlantedOn = DateTime.Today,
                PlantId = 0,
            });
            var result = await Modal.Show<AddSown>("New Sown", parameters).Result;
            if (!result.Cancelled)
            {
                await LoadData();
            }
        }

        public async Task LoadData()
        {
            Crop = await CropManager.GetById(int.Parse(Id));
            ImgSrc = CropManager.DownloadPlotImage(Crop.PlotImage);
            // add POI pins to img
            await InvokeAsync(StateHasChanged);
        }

        async Task RemoveSown(Sown sown)
        {
            var result = await Modal.Show<RemoveConfirmation>(
             $"Remove {sown.Plant.Name}, {sown.PlantedOn.ToShortDateString()}").Result;
            if (!result.Cancelled)
            {
                await SownManager.Delete(sown.Id);
                await LoadData();
            }
        }
        async Task EditSownItem(Sown sown)
        {
            var parameters = new ModalParameters();
            parameters.Add("Sown", sown);
            var result = await Modal.Show<EditSown>("Edit", parameters).Result;
            if (!result.Cancelled)
            {
                await LoadData();
            }
        }
    }
}