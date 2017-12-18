using Dapper;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Image : IImageRepository
    {
        public IEnumerable<Picture> GetImages(string name)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<Picture>($"SELECT * FROM dbo.StorePictures WHERE PictureName LIKE '%{name}%'");
            }
        }
    }
}
