using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Client.Services
{
	public class SownManager : APIRepository<SownDto>
	{
		public SownManager(IHttpClientFactory factory) : base(factory, "Sown")
		{

		}
	}

}
