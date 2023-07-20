using System;
using System.IO.Compression;
using System.Net;
using System.Web;
using System.Web.Optimization;

namespace PublicZaanVendor
{

    #region GZip Compression

    /// <summary>
    /// Class GZipBundle.
    /// </summary>
    public class GZipBundle : Bundle
    {
        public GZipBundle(string virtualPath, params IBundleTransform[] transforms)
            : base(virtualPath, null, transforms) { }

        public override BundleResponse CacheLookup(BundleContext context)
        {
            if (null != context) GZipEncodePage(context.HttpContext);
            return base.CacheLookup(context);
        }

        // Sets up the current page or handler to use GZip through a Response.Filter.
        public static void GZipEncodePage(HttpContextBase httpContext)
        {
            if (null != httpContext && null != httpContext.Request && null != httpContext.Response
                && (null == httpContext.Response.Filter
                || !(httpContext.Response.Filter is GZipStream || httpContext.Response.Filter is DeflateStream)))
            {
                // Is GZip supported?
                string acceptEncoding = httpContext.Request.Headers["Accept-Encoding"];
                if (null != acceptEncoding
                    && acceptEncoding.IndexOf(DecompressionMethods.GZip.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    httpContext.Response.Filter = new GZipStream(httpContext.Response.Filter, CompressionMode.Compress);
                    httpContext.Response.AddHeader("Content-Encoding", DecompressionMethods.GZip.ToString().ToLowerInvariant());
                }
                else if (null != acceptEncoding
                    && acceptEncoding.IndexOf(DecompressionMethods.Deflate.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    httpContext.Response.Filter = new DeflateStream(httpContext.Response.Filter, CompressionMode.Compress);
                    httpContext.Response.AddHeader("Content-Encoding", DecompressionMethods.Deflate.ToString().ToLowerInvariant());
                }

                // Allow proxy servers to cache encoded and unencoded versions separately
                httpContext.Response.AppendHeader("Vary", "Content-Encoding");
            }
        }
    }
    #endregion

    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
