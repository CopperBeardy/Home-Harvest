using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Client.Services
{
    public class PlantManager : APIRepository<PlantDto>
    {
        public PlantManager(IHttpClientFactory factory) : base(factory, "Plant")
        {
        }
    }
}
