using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace HomeHarvest.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DownloadController : ControllerBase
	{
		[HttpGet("{name}")]
		public IActionResult Download(string name)
		{
			var folderName = Path.Combine("StaticFiles", "Images");
			var pathToLoad = Path.Combine(Directory.GetCurrentDirectory(), folderName);
			var fullPath = Path.Combine(pathToLoad, name);

	
			using (var stream = new FileStream(fullPath, FileMode.Open))
			{
				using (var img = new Bitmap(stream))
				{		
					var memory = new MemoryStream();
					img.Save(memory, ImageFormat.Png);
					return File(memory,"image/png");
				}
				//file.CopyTo(stream);
			}
			
		}
	}
}
