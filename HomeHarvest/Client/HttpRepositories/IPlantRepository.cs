using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Client.HttpRepositories
{
    public interface IPlantRepository
    {
        Task<bool> Create(CreatePlantDto plant);
        Task<bool> Delete(int id);
        Task<List<PlantDto>> GetAll();
        Task<bool> Update(int id, PlantDto plant);
    }
}
