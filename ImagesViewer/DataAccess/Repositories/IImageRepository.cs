using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    interface IImageRepository
    {
        IEnumerable<Picture> GetImages(string name);

        void UploadPicture(Picture p);
    }
}
