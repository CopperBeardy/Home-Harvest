using ImageMagick;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace HomeHarvest.Server.Helpers
{
	public static class ImageConvertor
	{
        public static byte[] Sizer(IFormFile file)
		{        
            using (var memStream = file.OpenReadStream())
            {
                using (var image = new MagickImage(memStream))
                {
                    var size = new MagickGeometry(300, 500);
                    size.IgnoreAspectRatio = true;
                    image.Resize(size);
                    image.Quality = 10;
                    image.Format =  MagickFormat.Png;                 
                   return image.ToByteArray();
                }
            }
        }
	}
}
