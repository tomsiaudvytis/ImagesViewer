using System.Drawing;
using System.IO;
using System.Web;

namespace ImagesConverter
{
    public interface IConvert
    {
        byte[] ImgToBytes(string path);

        Image BytesToImage(byte[] bytes);

        byte[] FileBaseToBytes(Stream stream);
    }
}
