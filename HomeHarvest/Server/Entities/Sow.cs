using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHarvest.Server.Entities
{
	public class Sow
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		/// <summary>
		/// Plant that has been planted
		/// </summary>
		[Required(ErrorMessage = "You must select a type of plant")]
		public Plant Plant { get; set; }


		[Required(ErrorMessage = "The date you sowed this plant is required")]
		public DateTime PlantedOn { get; set; }
		/// <summary>
		/// Duration in which it takes the plant to flower or ready for harvesting
		/// </summary>
		[Required(ErrorMessage = "Length of time for flowering or Grow time is required")]
		public double GrowInWeeks { get; set; }

		/// <summary>
		/// Which years crop the plant belongs to
		/// </summary>
		public Crop Crop { get; set; }

		[ForeignKey(nameof(Crop))]
		public int CropId { get; set; }

		[ForeignKey(nameof(Plant))]
		public int PlantId { get; set; }
	}
}
