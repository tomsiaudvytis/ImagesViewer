using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Web.Mvc;
using ImagesConverter;
using System.Linq;

namespace ImagesViewerWeb.Controllers
{
    public class HomeController : Controller
    {
        private IConvert _imageConverter = new CustomImageConverter();
        private IImageRepository _imageRepo = new MsSQLImageRepository();
        IEnumerable<ImageModel> _images = null;

        public HomeController()
        {
            _images = _imageRepo.GetAllImages();
        }

        public ActionResult Home()
        {
            return View(_images);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ImageModel image)
        {
            if (image.File != null && image.PictureName != null)
            {
                bool isValidFormat = IsFormatValid(image.File.ContentType);

                if (!isValidFormat)
                {
                    ModelState.AddModelError("", "Select valid image file");
                    return View("Home", _images);
                }

                byte[] content = _imageConverter.FileBaseToBytes(image.File.InputStream);

                image.PictureContent = string.Join(" ", content);
                image.PictureID = Guid.NewGuid().ToString();
                image.Size = (int)content.LongLength;

                _imageRepo.UploadImage(image);
                _images = _imageRepo.GetAllImages();
            }
            else
            {
                if (image.PictureName == null)
                {
                    ModelState.AddModelError("", "Picture name is MUST !");
                }

                if (image.File == null)
                {
                    ModelState.AddModelError("", "Choose file !");
                }
            }
            return View("Home", _images);
        }

        public ActionResult Delete(string ID)
        {
            _imageRepo.DeleteImage(ID);
            _images = _imageRepo.GetAllImages();

            return View("Home", _images);
        }

        public ActionResult Select(string ID)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                IEnumerable<ImageModel> _picture = _imageRepo.GetImage(ID);

                string[] imgAsString = _picture.First().PictureContent.Split(' ');
                byte[] imgBytes = imgAsString.Select(byte.Parse).ToArray();
                ViewBag.ImageData = imgBytes;
            }

            return PartialView("Home", _images);
        }

        private bool IsFormatValid(string format)
        {
            List<string> validFormats = new List<string> { "image/jpeg", "image/gif", "image/png", "image/jpg", "image/tiff" };

            if (!validFormats.Contains(format.ToLower()))
            {
                return false;
            }

            return true;
        }
    }
}