using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HomeHarvest.Server.Entities;

namespace HomeHarvest.Shared.Entities
{
    public class Sown : BaseEntity
    {
       
        public Plant Plant { get; set; }

        [Required(ErrorMessage = "The date you sowed this plant is required")]
        public DateTime PlantedOn { get; set; }

        public int? PoiX { get; set; }
        public int? PoiY { get; set; }

        [Required]
        [ForeignKey(nameof(Plant))]
        public int PlantId { get; set; }

        [Required]
        public int CropId { get; set; }
    }
}
