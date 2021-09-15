using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeHarvest.Shared.Dtos
{
    public class CropDto : BaseDto
    {
        [JsonInclude]
        [Required(ErrorMessage = "You must enter a year for the crop being sowed")]
        public int Year { get; set; }

        [JsonInclude]
        public string Location { get; set; }

        [JsonInclude]
        public string PlotImage { get; set; }

        public byte[] Image { get; set; }

        [JsonInclude]
        public List<SownDto> Sowed { get; set; }

        public string LocationYear => $"{Location}, {Year}";
    }
}
