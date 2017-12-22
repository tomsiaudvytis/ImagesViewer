using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class MySqlImageRepository : IImageRepository
    {
        public void DeleteImage(string imageID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ImageModel> GetAllImages()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ImageModel> GetImage(string imageID)
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
