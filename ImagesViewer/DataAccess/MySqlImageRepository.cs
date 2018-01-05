using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    class MySQLImageRepository : IImageRepository
    {
        public void DeleteImage(string imageID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ImageModel> GetAllImages()
        {
            throw new NotImplementedException();
        }

        public ImageModel GetImage(string imageID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ImageModel> SearchImages(string name)
        {
            throw new NotImplementedException();
        }

        public void UploadImage(ImageModel image)
        {
            throw new NotImplementedException();
        }
    }
}
