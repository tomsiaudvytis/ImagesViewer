namespace DataAccess
{
    using Dapper;
    using DataAccess.Executors;
    using DataAccess.Models;
    using DataAccess.Repositories;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class MsSQLImageRepository : IImageRepository
    {
        private readonly ILog _logger;
        private readonly ISqlExecutor<ImageModel> _sqlExecutor;

        public MsSQLImageRepository(ILog Logger, ISqlExecutor<ImageModel> sqlExecutor)
        {
            this._logger = Logger;
            this._sqlExecutor = sqlExecutor;
        }

        public void DeleteImage(string imageID)
        {
            string sqlQuery = $"DELETE FROM dbo.StorePictures WHERE PictureID = '{imageID}'";

            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
                {
                    connection.Query<ImageModel>(sqlQuery);
                }
            }
            catch (System.Exception ex)
            {
                this._logger.Error(ex.Message);
            }

        }

        public IEnumerable<ImageModel> GetAllImages()
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures";
            try
            {
                return this._sqlExecutor.GetAllImages(sqlQuery);
            }
            catch (System.Exception ex)
            {
                this._logger.Error(ex.Message);
            }

            return null;
        }

        public ImageModel GetImage(string imageID)
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures WHERE PictureID = '{imageID}'";
            try
            {
                return this._sqlExecutor.GetImage(sqlQuery);
            }
            catch (System.Exception ex)
            {
                this._logger.Error(ex.Message);
            }

            return null;
        }

        public IEnumerable<ImageModel> SearchImages(string name)
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures WHERE PictureName LIKE '%{name}%'";
            try
            {
                return this._sqlExecutor.SearchImages(sqlQuery);
            }
            catch (System.Exception ex)
            {
                this._logger.Error(ex.Message);
            }

            return null;
        }

        public void UploadImage(ImageModel p)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO dbo.StorePictures (PictureID,PictureName, Size, UploadDate ,PictureContent) VALUES");
            sb.Append("(");
            sb.Append($"'{p.PictureID}', '{p.PictureName}', '{p.Size}', '{p.UploadDate}', '{p.PictureContent}' ");
            sb.Append(")");

            try
            {
                this._sqlExecutor.UploadImage(sb.ToString());
            }
            catch (Exception ex)
            {
                this._logger.Error(ex.Message);
            }
        }
    }
}
