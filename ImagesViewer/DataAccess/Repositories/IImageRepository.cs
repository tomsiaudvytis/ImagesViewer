using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    interface IImageRepository
    {
        IEnumerable<Picture> SearchImages(string name);

        void UploadImage(Picture p);

        IEnumerable<Picture> GetAllImages();

        IEnumerable<Picture> GetImage(string imageID);
    }
}
