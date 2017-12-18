using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    interface IImageRepository
    {
        IEnumerable<Picture> GetImages(string name);
    }
}
