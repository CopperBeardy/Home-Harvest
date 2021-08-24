namespace HomeHarvest.Server.Services
{
	public interface IBlobService
	{
		Task Upload(byte[] Image, string name);
		Task<bool> Delete(string name);

	}
}