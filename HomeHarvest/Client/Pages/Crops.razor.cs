using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using HomeHarvest.Client;
using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Client.Shared;
using HomeHarvest.Shared;
using HomeHarvest.Shared.Dtos;
using System.IO;
using DevExpress.Blazor;

namespace HomeHarvest.Client.Pages
{
    public partial class Crops
    {
        IEnumerable<CropDto> Values { get; set; }

        public string Img { get; set; }

        [Parameter]
        public IEnumerable<CropDto> CropDtos { get; set; } = new List<CropDto>();
        [Inject]
        public ICropRepository CropRepository { get; set; }

        public bool ShowRightPanel { get; set; } = false;
        [Parameter]
        public bool IsAddVisible { get; set; } = false;
        protected override Task OnInitializedAsync()
        {
            _ = LoadCrops();
            Values = CropDtos.Take(1);
            return base.OnInitializedAsync();
        }

        public async Task LoadCrops()
        {
            CropDtos = await CropRepository.GetAll();
            await InvokeAsync(StateHasChanged);
        }

        public async Task HandleNewCrop()
        {
            ShowRightPanel = true;
            IsAddVisible = true;
        }

        public async Task ToggleAddVisible(bool visible)
        {
            IsAddVisible = visible;

            await LoadCrops();
        }

        public async Task SelectedItemChanged(IEnumerable<CropDto> crops)
        {
            ShowRightPanel = true;

            Img = await CropRepository.DownloadPlotImage(crops.First().PlotImage);
        }
    }
}