using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using log4net;

namespace ImagesViewerWeb.Controllers
{
    public class HomeController : Controller
    {
        private IEnumerable<ImageModel> _images = null;
        private MvcApplication _app = null;

        public HomeController()
        {
            _app = new MvcApplication();

            try
            {
                _images = _app.imageController.GetAllImages();
            }
            catch (Exception ex)
            {
                _app._logger.Debug("Shit happens ! I am in HomeController constructor" + ex.Message);
                ModelState.AddModelError("", "Images load failed !" + ex.Message);
                _images = new List<ImageModel>();
            }
        }

        public ActionResult Home()
        {
            return View(_images);
        }

        [HttpPost]
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

                byte[] content = _app.imageController.FileBaseToBytes(image.File.InputStream);

                image.PictureContent = string.Join(" ", content);
                image.PictureID = Guid.NewGuid().ToString();
                image.Size = (int)content.LongLength;

                try
                {
                    _app.imageController.UploadImage(image);
                    _images = _app.imageController.GetAllImages();
                }
                catch (Exception e)
                {
                    _app._logger.Debug("Shit happens ! I am in HomeController Add()" + e.Message);
                    ModelState.AddModelError("", "Image adding failed !" + e.Message);
                }

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
            _app.imageController.DeleteImage(ID);
            try
            {
                _images = _app.imageController.GetAllImages();
            }
            catch (Exception ex)
            {
                _app._logger.Debug("Shit happens ! I am in HomeController Delete()" + ex.Message);
                ModelState.AddModelError("", "Images load failed !" + ex.Message);
                _images = new List<ImageModel>();
            }

            return View("Home", _images);
        }

        public ActionResult Select(string ID)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                byte[] imgBytes = this._app.imageController.ImageAsByteArr(ID);
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