using DataAccess;
using DataAccess.Repositories;
using ImagesConverter;
using log4net;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ImagesViewerWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public IConvert ImageConverter = new CustomImageConverter();
        public IImageRepository ImageRepository = new MsSQLImageRepository();
        public ILog Logger = LogManager.GetLogger(typeof(Controller));

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Logger.Debug("Application started");
        }
    }
}
