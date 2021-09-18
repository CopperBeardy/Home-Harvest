using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Client.Services
{
    public class PlantManager : APIRepository<PlantDto>
    {
        public PlantManager(HttpClient client) : base(client, "Plant")
        {
        }
    }
}
