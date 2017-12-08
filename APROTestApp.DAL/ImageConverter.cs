using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace APROTestApp.DAL
{
    static class ImageConverter
    {
        public static byte[] Convert(Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Gif);
                return stream.ToArray();
            }
        }
        public static Image Convert(byte[] imageArray)
        {
            using (var stream = new MemoryStream(imageArray))
            {
                Image returnImage = Image.FromStream(stream);
                return returnImage;
            }
        }
    }
}
