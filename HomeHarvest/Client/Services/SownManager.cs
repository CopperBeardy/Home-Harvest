
using HomeHarvest.Shared.Entities;

namespace HomeHarvest.Client.Services
{
	public class SownManager : APIManager<Sown>
	{
		public SownManager(HttpClient client) : base(client, "Sown")
		{

		}
	}

}
