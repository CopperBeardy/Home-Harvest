using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHarvest.Server.Entities
{
	public class Sown
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



		[ForeignKey(nameof(Plant))]
		public int PlantId { get; set; }
	}
}
