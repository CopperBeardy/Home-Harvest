using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Client.Services
{
    public class PlantManager : APIManager<PlantDto>
    {
        public PlantManager(HttpClient client) : base(client, "Plant")
        {
        }
    }
}
