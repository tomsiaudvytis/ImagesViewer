using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DataAccess.Models;
using DataAccess.Repositories;
using ImagesConverter;
using System.Data;
using Dapper;
using System.Text;
using DataAccess;

namespace ImageController.Controllers
{
    public class ImageController
    {
        private IConvert _converter { get; set; }
        private IImageRepository _imageRepo { get; set; }

        public ImageController(IConvert converter, IImageRepository imageRepo)
        {
            this._converter = converter;
            this._imageRepo = imageRepo;
        }

        public byte[] ImgToBytes(string path)
        {
            Image img = Image.FromFile(path);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, img.RawFormat);
            return ms.ToArray();
        }

        public void UploadImage(ImageModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("INSERT INTO dbo.StorePictures (PictureID,PictureName, Size, UploadDate ,PictureContent) VALUES");
                sb.Append("(");
                sb.Append($"'{model.PictureID}', '{model.PictureName}', '{model.Size}', '{model.UploadDate}', '{model.PictureContent}' ");
                sb.Append(")");

                connection.Query<ImageModel>(sb.ToString());
            }
        }

        public Image BytesToImage(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            Image returnImage = Image.FromStream(ms);
            return returnImage; ;
        }

        public byte[] FileBaseToBytes(Stream stream)
        {
            byte[] content;

            using (var reader = new BinaryReader(stream))
            {
                content = reader.ReadBytes((int)stream.Length);
            }

            return content;
        }

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

        public IEnumerable<ImageModel> GetImage(string ID)
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures WHERE PictureID = '{ID}'";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<ImageModel>(sqlQuery);
            }
        }

        public IEnumerable<ImageModel> SearchImages(string text)
        {
            string sqlQuery = $"SELECT * FROM dbo.StorePictures WHERE PictureName LIKE '%{text}%'";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("PicturesDb")))
            {
                return connection.Query<ImageModel>(sqlQuery);
            }
        }
    }
}
