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
using HomeHarvest.Client.Models;
using HomeHarvest.Client.Services;
using HomeHarvest.Client.Shared;
using HomeHarvest.Client.Components;
using HomeHarvest.Shared;
using HomeHarvest.Shared.Dtos;
using HomeHarvest.Shared.Enums;
using System.IO;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;

namespace HomeHarvest.Client.Components
{
    public partial class EditPlant
    {
        [Inject]
        PlantManager PlantManager { get; set; }
        [CascadingParameter]
        BlazoredModalInstance ModalInstance { get; set; }
        [Parameter]
       public  PlantDto Plant { get; set; } 
        async Task Cancel() => await ModalInstance.CancelAsync();
     

        public async Task HandleValidSubmit()
        {
            await PlantManager.Update(Plant);
            await ModalInstance.CloseAsync(ModalResult.Ok("success"));
          
        }
    }
}