using HomeHarvest.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeHarvest.Shared.Dtos
{
	public class PlantDto
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Type of plant 
		/// </summary>
		public Genus Genus { get; set; }
		/// <summary>
		/// The name in which the plant is more generally known by
		/// </summary>
		[Required(ErrorMessage = "A common name of the plant is required")]
		[StringLength(150, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 150 characters")]
		public string Name { get; set; }




	}
}
