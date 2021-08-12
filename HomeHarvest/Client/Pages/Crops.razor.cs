using HomeHarvest.Client.HttpRepositories;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace HomeHarvest.Client.Pages
{
	public partial class Crops
	{
		[Inject]
		public ICropRepository CropRepository { get; set; }
		public IEnumerable<CropDto> CropDtos { get; set; } = new List<CropDto>();
		IEnumerable<CropDto> Values { get; set; }
		protected string Img { get; set; }

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

		public async Task SelectedItemChanged(IEnumerable<CropDto> crops)
		{
			Img = await CropRepository.DownloadPlotImage(crops.First().PlotImage);
		}
	}

}