using HomeHarvest.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeHarvest.Shared
{
	public class CropDto
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// Year in which the crop was planted in
		/// </summary>
		[Required(ErrorMessage ="You must enter a year for the crop being sowed")]
		public int Year { get; set; }

		/// <summary>
		/// Collection of plants that were planted in the crop year
		/// </summary>
		public virtual ICollection<SowDto> Sowed { get; set; }
	}
}
