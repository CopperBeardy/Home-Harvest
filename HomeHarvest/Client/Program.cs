using Blazored.Modal;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using HomeHarvest.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Diagnostics.CodeAnalysis;


namespace HomeHarvest.Client
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


            builder.Services.AddHttpClient("HomeHarvest.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient( "HomeHarvest.ServerAPI"));

            builder.Services.AddBlazoredModal();
            builder.Services
   .AddBlazorise(options =>
   {
       options.ChangeTextOnKeyPress = true;
   })
   .AddBootstrapProviders()
   .AddFontAwesomeIcons();


            builder.Services.AddApiAuthorization();

            builder.Services.AddScoped<CropManager>();
            builder.Services.AddScoped<SownManager>();
            builder.Services.AddScoped<PlantManager>();
            await builder.Build().RunAsync();
        }
    }
}
