using Autofac;
using BeautyBook.Common;
using PublicZaanVendor.Infrastructure;
using PublicZaanVendor.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace PublicZaanVendor
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
            builder.RegisterType<BindDropdown>().As<BindDropdown>();
            builder.RegisterType<BaseController>().As<BaseController>();
            Bootstrapper.Resolve(builder);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Languages"];
            if (cookie != null && cookie.Value != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
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
