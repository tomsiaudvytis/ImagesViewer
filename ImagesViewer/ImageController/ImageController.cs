using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DataAccess.Models;
using DataAccess.Repositories;
using ImagesConverter;
using System.Data;
using System.Linq;
using log4net;

namespace ImageController.Controllers
{
    public class ImageController
    {
        private readonly ILog _logger;

        private IConvert _converter { get; set; }
        private IImageRepository _imageRepo { get; set; }

        public ImageController(IConvert converter, IImageRepository imageRepo, ILog logger)
        {
            this._logger = logger;
            this._converter = converter;
            this._imageRepo = imageRepo;
        }

        public void UploadImage(ImageModel model)
        {
            this._imageRepo.UploadImage(model);
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

        public void UploadImage(string imgPath)
        {
            byte[] imgToUploadInBytes = this._converter.ImgToBytes(imgPath);

            ImageModel imageToUpload = new ImageModel()
            {
                PictureContent = string.Join(" ", imgToUploadInBytes),
                PictureID = Guid.NewGuid().ToString(),
                PictureName = imgPath,

                //Size ruffly in Megabytes
                Size = imgToUploadInBytes.Count() / 1000000
            };

            this.UploadImage(imageToUpload);
        }

        public void DeleteImage(string imageID)
        {
            _imageRepo.DeleteImage(imageID);
        }

        public IEnumerable<ImageModel> GetAllImages()
        {
            try
            {
                return this._imageRepo.GetAllImages();
            }
            catch (Exception ex)
            {
                this._logger.Error(ex.Message);
            }

            return null;
        }

        public Image GetImage(string ID)
        {
            var imageFromDb = _imageRepo.GetImage(ID);

            string[] currentImgAsStringArr = imageFromDb.PictureContent.Split(' ');
            byte[] currentImbAsBytesArr = currentImgAsStringArr.Select(byte.Parse).ToArray();

            return this._converter.BytesToImage(currentImbAsBytesArr);
        }

        public byte[] ImageAsByteArr(string ID)
        {
            var imageFromDb = _imageRepo.GetImage(ID);

            string[] currentImgAsStringArr = imageFromDb.PictureContent.Split(' ');
            return currentImgAsStringArr.Select(byte.Parse).ToArray();
        }

        public IEnumerable<ImageModel> SearchImages(string text)
        {
            return this._imageRepo.SearchImages(text);
        }
    }
}
