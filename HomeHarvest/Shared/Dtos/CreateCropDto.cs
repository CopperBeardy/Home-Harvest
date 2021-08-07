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
		public string Location { get; set; }
		/// <summary>
		/// Image name of the plot for the current crop
		/// </summary>
		[JsonInclude]
		public string PlotImage { get; set; }
	}
}
