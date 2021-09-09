using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeHarvest.Shared.Dtos
{
	
	public class CropDto
	{
		public int Id { get; set; }

		/// <summary>
		/// Year in which the crop was planted in
		/// </summary>
		[Required(ErrorMessage = "You must enter a year for the crop being sowed")]
		public int Year { get; set; }

		public string Location { get; set; }
		/// <summary>
		/// Image name of the plot for the current crop
		/// </summary>

		public string LocationYear => $"{Location}, {Year}";
		public string PlotImage { get; set; }

		public byte[] Image { get; set; }
		/// <summary>
		/// Collection of plants that were planted in the crop year
		/// </summary>

		[JsonInclude]
		public  List<SownDto> Sowed { get; set; } 

	}
}
