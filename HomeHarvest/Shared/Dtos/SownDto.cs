using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeHarvest.Shared.Dtos
{
	public class SownDto
	{
		[JsonInclude]
		public int Id { get; set; }
		/// <summary>
		/// Plant that has been planted
		/// </summary>

		[JsonInclude]
		public PlantDto Plant { get; set; }

		/// <summary>
		/// Date on which the plant was planted
		/// </summary>
		[Required(ErrorMessage = "The date you sown this plant is required")]
		[JsonInclude]
		public DateTime PlantedOn { get; set; }
		
		[ForeignKey(nameof(Plant))]
		public int PlantId { get; set; }

		[JsonIgnore]
		public  CropDto Crop { get; set; }
		[ForeignKey(nameof(Crop))]
		public int CropId { get; set; }
	}
}
