using System;

namespace DataAccess.Models
{
    public class ImageModel
    {
        public string PictureID { get; set; }
        public string PictureName { get; set; }
        public int Size { get; set; }
        public string UploadDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        public string PictureContent { get; set; }
    }
}
