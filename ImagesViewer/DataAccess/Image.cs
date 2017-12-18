using Dapper;
using DataAccess.Models;
using DataAccess.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess
{
    public class Image : IImageRepository
    {
        public IEnumerable<Picture> GetAllImages()
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<Picture>(sqlQuery);
            }
        }

        public IEnumerable<Picture> GetImage(string imageID)
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures WHERE PictureID = '{imageID}'";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<Picture>(sqlQuery);
            }
        }

        public IEnumerable<Picture> SearchImages(string name)
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures WHERE PictureName LIKE '%{name}%'";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<Picture>(sqlQuery);
            }
        }

        public void UploadImage(Picture p)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("INSERT INTO dbo.StorePictures (PictureID,PictureName, Size, UploadDate ,PictureContent) VALUES (");
                sb.Append($"'{p.PictureID}', '{p.PictureName}', '{p.Size}', '{p.UploadDate}', '{p.PictureContent}' )");

                connection.Query<Picture>(sb.ToString());
            }
        }
    }
}
