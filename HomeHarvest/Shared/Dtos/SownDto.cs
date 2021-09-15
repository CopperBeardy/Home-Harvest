using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HomeHarvest.Shared.Dtos
{
    public class SownDto : BaseDto
    {
        [JsonInclude]
        public PlantDto Plant { get; set; }

        [JsonInclude]
        public int? PoiX { get; set; }

        [JsonInclude]
        public int? PoiY { get; set; }

        [Required(ErrorMessage = "The date you sown this plant is required")]
        [JsonInclude]
        public DateTime PlantedOn { get; set; }

        [ForeignKey(nameof(Plant))]
        [JsonInclude]
        public int PlantId { get; set; }

        [JsonIgnore]
        public CropDto Crop { get; set; }
        [ForeignKey(nameof(Crop))]

        [JsonInclude]
        public int CropId { get; set; }
    }
}
