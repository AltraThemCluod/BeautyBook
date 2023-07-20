using Elmah.Contrib.WebApi;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Batch;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using BeautyBook.APICommon;

namespace BeautyBookApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsByDomainAttribute();
            ////var cors = new EnableCorsAttribute("https://demo-bo.mavent.tech", "*", "*");
            //config.EnableCors(cors);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);


            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());

            config.Routes.MapHttpRoute(
            name: "help_ui_shortcut",
            routeTemplate: "docs",
            defaults: null,
            constraints: null,
            handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "docs"));

            // Web API routes
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.MessageHandlers.Add(new GlobalMessageLogger());

            config.Services.Replace(typeof(IHttpControllerSelector), new ContentNegotiationVersioningSelector(config));

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            //config.Filters.Add(new Authenticate());
        }

        //public static HttpConfiguration Register()
        //{
        //    HttpConfiguration config = new HttpConfiguration();
        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpBatchRoute(
        //        routeName: "WebApiBatch",
        //        routeTemplate: "api/$batch",
        //        batchHandler: new DefaultHttpBatchHandler(GlobalConfiguration.DefaultServer));

        //    config.Formatters.Add(new XlsxMediaTypeFormatter());

        //    config.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
        //    config.Services.Add(typeof(IExceptionLogger),
        //        new SlabLogExceptionLogger(UnityConfig.GetConfiguredContainer().Resolve<ILogProvider>()));

        //    config.EnableSystemDiagnosticsTracing();
        //    config.Services.Replace(typeof(ITraceWriter), new SlabTraceWriter());

        //    WebApiUnityActionFilterProvider.RegisterFilterProviders(config);
        //    return config;
        //}
    }
}
