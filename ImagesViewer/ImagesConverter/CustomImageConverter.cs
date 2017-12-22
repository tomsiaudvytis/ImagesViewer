using System.Drawing;
using System.IO;

namespace ImagesConverter
{
    public class CustomImageConverter : IConvert
    {
        public Image BytesToImage(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public byte[] FileBaseToBytes(Stream stream)
        {
            byte[] content;

            using (var reader = new BinaryReader(stream))
            {
                content = reader.ReadBytes((int)stream.Length);
            }

            return content;
        }

        public byte[] ImgToBytes(string path)
        {
            Image img = Image.FromFile(path);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, img.RawFormat);
            return ms.ToArray();
        }
    }
}
