// ----------------------------------------------------------------------
// <copyright file="bootstrapper.cs" company="Premiere Digital Services">
//     Copyright statement. All right reserved
// </copyright>
//
// ------------------------------------------------------------------------

namespace PublicZaanVendor
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using BeautyBook.Services;
    using System.Web.Mvc;

    internal sealed class Bootstrapper
    {
        public static void Resolve(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<ServiceModule>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            ////// Create the dependency resolver.
            //var resolver = new Auto(container);

            ////// Configure Web API with the dependency resolver.
            //GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}