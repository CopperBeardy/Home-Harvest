using HomeHarvest.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace HomeHarvest.Shared.Dtos
{
    [ExcludeFromCodeCoverage]
    public class PlantDto : BaseDto
    {
        [JsonInclude]
        public Genus Genus { get; set; }

        [JsonInclude]
        [Required(ErrorMessage = "A common name of the plant is required")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 150 characters")]
        public string Name { get; set; }

        [JsonInclude]
        [Required(ErrorMessage = "Length of time for flowering or Grow time is required")]
        public double GrowInWeeks { get; set; }
    }
}
