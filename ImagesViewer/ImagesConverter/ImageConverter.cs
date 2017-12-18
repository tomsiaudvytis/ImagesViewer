using System.Drawing;
using System.IO;

namespace ImagesConverter
{
    public class CustomImageConverter : IConvert
    {

        public Image BytesToImage(byte[] bytes)
        {
            throw new System.NotImplementedException();
        }

        public byte[] ImgToBytes(string path)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}
