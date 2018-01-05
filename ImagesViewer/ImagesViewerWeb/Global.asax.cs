using DataAccess;
using DataAccess.Repositories;
using ImagesConverter;
using log4net;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ImagesViewerWeb
{
    using DataAccess.Executors;
    using DataAccess.Models;
    using ImageController.Controllers;

    public class MvcApplication : System.Web.HttpApplication
    {
        public ILog _logger;
        public ImageController imageController;
        private ISqlExecutor<ImageModel> _sqlExecutor;

        public MvcApplication()
        {
            _logger = LogManager.GetLogger(typeof(Controller));
            this._sqlExecutor = new SqlExecutor();
            this.imageController = new ImageController(new CustomImageConverter(this._logger), new MsSQLImageRepository(this._logger, this._sqlExecutor), this._logger);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _logger.Debug("Application started");
        }
    }
}
