using System;

namespace DataAccess.Models
{
    public class Picture
    {
        public int PictureID { get; set; }
        public string PictureName { get; set; }
        public int Size { get; set; }
        public DateTime UploadDate { get; set; }
        public string PictureContent { get; set; }
    }
}
