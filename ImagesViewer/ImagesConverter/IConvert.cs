using System.Drawing;

namespace ImagesConverter
{
    interface IConvert
    {
        byte[] ImgToBytes(string path);

        Image BytesToImage(byte[] bytes);
    }
}
