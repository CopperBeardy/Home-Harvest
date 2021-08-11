using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeHarvest.Shared.Dtos
{
	public class CreateCropDto
	{
		[JsonInclude]
		[Required(ErrorMessage ="Location of the plot is required")]
		public string Location { get; set; }
		/// <summary>
		/// Image name of the plot for the current crop
		/// </summary>
		[JsonInclude]
		[Required(ErrorMessage ="A picture of the plot is required")]
		public string PlotImage { get; set; }
	}
}
