using DataAccess;
using DataAccess.Repositories;
using System.Web.Mvc;

namespace ImagesViewerWeb.Controllers
{
    public class HomeController : Controller
    {
        private IImageRepository _imageRepo = new ImageRepository();

        public ActionResult Home()
        {
            var images = _imageRepo.GetAllImages();

            return View(images);
        }
    }
}