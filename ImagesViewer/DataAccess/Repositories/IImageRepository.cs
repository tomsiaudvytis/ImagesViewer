using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IImageRepository
    {
        IEnumerable<ImageModel> SearchImages(string name);

        void UploadImage(ImageModel image);

        IEnumerable<ImageModel> GetAllImages();

        IEnumerable<ImageModel> GetImage(string imageID);

        void DeleteImage(string imageID);
    }
}
