using ImageMagick;

namespace HomeHarvest.Server.Services
{
	public static class ImageConvertor
	{
		public static byte[] Sizer(byte[] Image)
		{

			using (var image = new MagickImage(Image))
			{
				var size = new MagickGeometry(300, 500);
				size.IgnoreAspectRatio = true;
				image.Resize(size);
				image.Quality = 10;
				image.Format = MagickFormat.Png;
				return image.ToByteArray();
			}

		}
	}
}
