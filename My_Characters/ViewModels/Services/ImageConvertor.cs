using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Characters.ViewModels.Services
{
    public class ImageConvertor
    {
        public byte[] ConvertImageToByteArray(Image image )
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public Image ConverByteToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var image = Image.FromStream(ms);
                return image;
            }
        }
    }
}
