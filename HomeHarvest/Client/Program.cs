using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using HomeHarvest.Client.HttpRepositories;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace HomeHarvest.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddHttpClient("HomeHarvest.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
			builder.Services
				.AddBlazorise(options =>
				{
					options.ChangeTextOnKeyPress = true;
				})
				.AddBootstrapProviders()
				.AddFontAwesomeIcons();


			// Supply HttpClient instances that include access tokens when making requests to the server project
			builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("HomeHarvest.ServerAPI"));
			
			builder.Services.AddApiAuthorization();
			builder.Services.AddScoped<ICropRepository, CropRepository>();
			builder.Services.AddScoped<ISownRepository, SownRepository>();
            builder.Services.AddScoped<IPlantRepository, PlantRepository>();
            await builder.Build().RunAsync();
		}
	}
}
