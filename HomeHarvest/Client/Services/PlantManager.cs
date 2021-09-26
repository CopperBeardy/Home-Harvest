
using HomeHarvest.Shared.Entities;

namespace HomeHarvest.Client.Services
{
    public class PlantManager : APIManager<Plant>
    {
        public PlantManager(HttpClient client) : base(client, "Plant")
        {
        }
    }
}
