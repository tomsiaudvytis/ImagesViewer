namespace DataAccess.Executors
{
    using Dapper;
    using DataAccess.Models;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public interface ISqlExecutor<T>
    {
        IEnumerable<T> GetAllImages(string sqlCommand);
        T GetImage(string sqlCommand);
        IEnumerable<ImageModel> SearchImages(string sqlCommand);
        void UploadImage(string sqlCommand);
    }

    public class SqlExecutor : ISqlExecutor<ImageModel>
    {
        public IEnumerable<ImageModel> GetAllImages(string sqlCommand)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<ImageModel>(sqlCommand);
            }
        }

        public ImageModel GetImage(string sqlCommand)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<ImageModel>(sqlCommand).FirstOrDefault();
            }
        }

        public IEnumerable<ImageModel> SearchImages(string sqlCommand)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<ImageModel>(sqlCommand);
            }
        }

        public void UploadImage(string sqlCommand)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                connection.Query<ImageModel>(sqlCommand);
            }
        }
    }
}
