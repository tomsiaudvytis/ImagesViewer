using Dapper;
using DataAccess.Models;
using DataAccess.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess
{
    public class ImageRepository : IImageRepository
    {
        public void DeleteImage(string imageID)
        {
            string sqlQuery = $"DELETE FROM dbo.StorePictures WHERE PictureID = '{imageID}'";
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                connection.Query<ImageModel>(sqlQuery);
            }
        }

        public IEnumerable<ImageModel> GetAllImages()
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<ImageModel>(sqlQuery);
            }
        }

        public IEnumerable<ImageModel> GetImage(string imageID)
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures WHERE PictureID = '{imageID}'";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<ImageModel>(sqlQuery);
            }
        }

        public IEnumerable<ImageModel> SearchImages(string name)
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures WHERE PictureName LIKE '%{name}%'";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<ImageModel>(sqlQuery);
            }
        }

        public void UploadImage(ImageModel p)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("INSERT INTO dbo.StorePictures (PictureID,PictureName, Size, UploadDate ,PictureContent) VALUES (");
                sb.Append($"'{p.PictureID}', '{p.PictureName}', '{p.Size}', '{p.UploadDate}', '{p.PictureContent}' )");

                connection.Query<ImageModel>(sb.ToString());
            }
        }
    }
}
