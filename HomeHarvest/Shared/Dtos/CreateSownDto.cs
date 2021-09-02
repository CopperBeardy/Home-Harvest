using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeHarvest.Shared.Dtos
{
	public class CreateSownDto
	{
		[Required]
		public PlantDto Plant { get; set; }
		[Required(ErrorMessage = "Date of planting is required")]
		[DataType(DataType.Date)]
		public DateTime PlantedOn { get; set; }
		[Required (ErrorMessage ="Number of average weeks till bloom/harvest")]
		public double GrowWeeks { get; set; }


		public int CropId { get; set; }

	}
}
