using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeHarvest.Shared.Dtos
{
	public class SowDto
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		/// <summary>
		/// Plant that has been planted
		/// </summary>
		[Required(ErrorMessage = "You must select a type of plant")]
		public PlantDto Plant { get; set; }

		/// <summary>
		/// Date on which the plant was planted
		/// </summary>
		[Required(ErrorMessage = "The date you sowed this plant is required")]
		public DateTime PlantedOn { get; set; }
		/// <summary>
		/// Duration in which it takes the plant to flower or ready for harvesting
		/// </summary>
		[Required(ErrorMessage = "Length of time for flowering or Grow time is required")]
		public double GrowInWeeks { get; set; }

		/// <summary>
		/// Returns the calculated Harvest using PlantedOn and GrowInWeeks variables
		/// </summary>
		public DateTime HarvestDate => PlantedOn.AddDays(GrowInWeeks * 7.0);

		/// <summary>
		/// Which years crop the plant belongs to
		/// </summary>
		public CropDto Crop { get; set; }

		[ForeignKey(nameof(Crop))]
		public int CropId { get; set; }

		[ForeignKey(nameof(Plant))]
		public int PlantId { get; set; }
	}
}
