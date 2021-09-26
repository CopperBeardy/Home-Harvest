using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Client.Services
{
	public class SownManager : APIManager<SownDto>
	{
		public SownManager(HttpClient client) : base(client, "Sown")
		{

		}
	}

}
