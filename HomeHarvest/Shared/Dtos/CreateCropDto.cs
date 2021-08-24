using System.ComponentModel.DataAnnotations;

namespace HomeHarvest.Shared.Dtos
{
	public class CreateCropDto
	{

		[Required(ErrorMessage = "Location of the plot is required")]
		public string Location { get; set; }
		/// <summary>
		/// Image name of the plot for the current crop
		/// </summary>

		[Required(ErrorMessage = "A picture of the plot is required")]
		public string PlotImage { get; set; }

		[Required]
		public byte[] Image { get; set; }

	}
}
