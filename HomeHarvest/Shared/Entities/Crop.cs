using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeHarvest.Server.Entities;

namespace HomeHarvest.Shared.Entities
{
    public class Crop : BaseEntity
    {


        /// <summary>
        /// Year in which the crop was planted in
        /// </summary>
        [Required(ErrorMessage = "You must enter a year for the crop being sowed")]
        public int Year { get; set; }
        [Required]
        public string Location { get; set; }
        /// <summary>
        /// Image name of the plot for the current crop
        /// </summary>
                [Required]
        public string PlotImage { get; set; }
        [NotMapped]
        public string LocationYear => $"{Location}, {Year}";
        [NotMapped]
        public byte[] Image { get; set; }
        /// <summary>
        /// Collection of plants that were planted in the crop year
        /// </summary>
        public virtual ICollection<Sown> Sowed { get; set; }
    }
}
