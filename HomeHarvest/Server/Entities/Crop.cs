using System.ComponentModel.DataAnnotations;

namespace HomeHarvest.Server.Entities
{
    public class Crop : BaseEntity
    {


        /// <summary>
        /// Year in which the crop was planted in
        /// </summary>
        [Required(ErrorMessage = "You must enter a year for the crop being sowed")]
        public int Year { get; set; }
        public string Location { get; set; }
        /// <summary>
        /// Image name of the plot for the current crop
        /// </summary>
        public string PlotImage { get; set; }
        /// <summary>
        /// Collection of plants that were planted in the crop year
        /// </summary>	
        public virtual ICollection<Sown> Sowed { get; set; }
    }
}
