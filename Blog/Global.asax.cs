using Autofac;
using BeautyBook.Common;
using Blog.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Blog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ContainerBuilder builder = new ContainerBuilder();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            ModelMetadataProviders.Current = new CachedDataAnnotationsModelMetadataProvider();
            Bootstrapper.Resolve(builder);
        }


        /// <summary>
        /// This method is called on application end request
        /// </summary>
        public void Application_EndRequest()
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Code that runs when an unhandled error occurs
            Exception ErrorInfo = Server.GetLastError().GetBaseException();
            CommonHelper.LogError(Server.MapPath("~/ErrorLog/ErrorLog.txt"), ErrorInfo);
            //Infrastructure.ErrorLogHelper.Log(ErrorInfo);
            //Server.ClearError();
            //Response.Redirect(ConfigItems.HostURL + Pages.Controllers.Account + "/" + Pages.Actions.Error);
        }
    }
}
