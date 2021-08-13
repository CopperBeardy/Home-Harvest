using System.ComponentModel.DataAnnotations;

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


		public string LocationYear => $"{Location}, {Year}";

		/// <summary>
		/// Image name of the plot for the current crop
		/// </summary>

		public string PlotImage { get; set; }


		/// <summary>
		/// Collection of plants that were planted in the crop year
		/// </summary>

		public virtual ICollection<SowDto> Sowed { get; set; }
	}
}
