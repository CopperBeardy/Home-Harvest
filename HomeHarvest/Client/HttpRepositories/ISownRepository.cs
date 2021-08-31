using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Client.HttpRepositories
{
	public interface ISownRepository
	{
		Task<bool> Create(CreateSownDto Sow);
		Task<bool> Delete(int id);
		Task<List<SownDto>> GetAll();
		Task<SownDto> Get(int id);
		Task<bool> Update(int id, SownDto Sow);

	}
}
