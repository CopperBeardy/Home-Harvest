using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HomeHarvestTests.Client.ServicesTests
{
	[ExcludeFromCodeCoverage]
	public class ApiRepositoryTests
	{
		//const string url = "https://localhost:5003/api/";
		//[Fact]
		//public async Task GetEntity()
		//{
		//	var handler = new Mock<HttpMessageHandler>();
		//	var client = handler.CreateClient();
		//	var manager = new PlantManager(client);
		//	var plant = Mock.Of<PlantDto>();
		//	plant.Id = 1;
		//	// plant = new PlantDto()
		//	//{
		//	//	Id = 1,
		//	//	GrowInWeeks = 10,
		//	//	 Genus= Genus.Flower,
		//	//	 Name ="Daisy"
		//	//};
		//	handler.SetupRequest(HttpMethod.Get,$"{url}plant")
		//		.ReturnsResponse(JsonConvert.SerializeObject(plant));



		//	var result =await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{url}plant"));
		//	var actual = JsonConvert.DeserializeObject<PlantDto>(await result.Content.ReadAsStringAsync());


		//	Assert.Equal(1, actual.Id);

		//}
	}
}
