namespace ImagesConverter
{
    interface IConvert
    {
        byte[] ImgToBytes(string path);

        System.Drawing.Image BytesToImage(byte[] bytes);
    }
}
