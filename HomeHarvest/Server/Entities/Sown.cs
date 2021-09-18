using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeHarvest.Server.Entities
{
    public class Sown : BaseEntity
    {
        [Required(ErrorMessage = "You must select a type of plant")]
        public Plant Plant { get; set; }

        [Required(ErrorMessage = "The date you sowed this plant is required")]
        public DateTime PlantedOn { get; set; }

        public int? PoiX { get; set; }
        public int? PoiY { get; set; }

        [ForeignKey(nameof(Plant))]
        public int PlantId { get; set; }

        [JsonIgnore]
        public Crop Crop { get; set; }

        [ForeignKey(nameof(Crop))]
        public int CropId { get; set; }
    }
}
