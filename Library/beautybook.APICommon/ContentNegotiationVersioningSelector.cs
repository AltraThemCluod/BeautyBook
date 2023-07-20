//-----------------------------------------------------------------------
// <copyright file="ContentNegotiationVersioningSelector.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;    
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    using System.Web.Http.Routing;

    /// <summary>
    /// Class controller selector for content negotiation
    /// </summary>
    public class ContentNegotiationVersioningSelector : DefaultHttpControllerSelector
    {
        /// <summary>
        /// The latest version
        /// </summary>
        private string latestVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentNegotiationVersioningSelector"/> class.
        /// </summary>
        /// <param name="httpConfiguration">The HTTP configuration.</param>
        public ContentNegotiationVersioningSelector(HttpConfiguration httpConfiguration)
            : base(httpConfiguration)
        {
        }

        /// <summary>
        /// extension to default controller selector
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <returns>
        /// The <see cref="T:System.Web.Http.Controllers.HttpControllerDescriptor" /> instance for the given <see cref="T:System.Net.Http.HttpRequestMessage" />.
        /// </returns>
        /// <exception cref="System.Web.Http.HttpResponseException">
        /// Throw not found exception
        /// </exception>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor controllerDescriptor = null;

            // get list of controllers provided by the default selector
            IDictionary<string, HttpControllerDescriptor> controllers = GetControllerMapping();
            
            // get request route data
            IHttpRouteData routeData = request.GetRouteData();

            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            // get api version from accept header
            var apiVersion = this.GetVersion(request);

            // check if this route is actually an attribute route
            IEnumerable<IHttpRouteData> attributeRoutes = routeData.GetSubRoutes();

            if (attributeRoutes == null)
            {
                string controllerName = GetRouteVariable<string>(routeData, "controller");
                if (controllerName == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                string versionControllerName = string.Concat(controllerName, "V", apiVersion);

                if (controllers.TryGetValue(versionControllerName, out controllerDescriptor))
                {
                    return controllerDescriptor;
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
            else
            {
                // find all controller descriptors whose controller type names end with
                // the following suffix (example: ControllerV1)
                string versionControllerName = string.Concat("V", apiVersion);

                IEnumerable<IHttpRouteData> filteredAttributeRoutes = attributeRoutes
                    .Where(attrRouteData =>
                    {
                        HttpControllerDescriptor currentDescriptor = GetControllerDescriptor(attrRouteData);

                        bool match = currentDescriptor.ControllerName.EndsWith(versionControllerName);

                        if (match && (controllerDescriptor == null))
                        {
                            controllerDescriptor = currentDescriptor;
                        }

                        return match;
                    });

                routeData.Values["MS_SubRoutes"] = filteredAttributeRoutes.ToArray();
            }

            return controllerDescriptor;
        }

        /// <summary>
        /// gets the controller descriptor for attribute routes.
        /// </summary>
        /// <param name="routeData">The route data.</param>
        /// <returns>Controller descriptor</returns>
        private static HttpControllerDescriptor GetControllerDescriptor(IHttpRouteData routeData)
        {
            return ((HttpActionDescriptor[])routeData.Route.DataTokens["actions"]).First().ControllerDescriptor;
        }

        /// <summary>
        /// gets value from the route data, if present.
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="routeData">The route data.</param>
        /// <param name="name">The name.</param>
        /// <returns>The route parameter value</returns>
        private static T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result = null;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T)result;
            }

            return default(T);
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The version
        /// </returns>
        private string GetVersion(HttpRequestMessage request)
        {
            var acceptHeader = request.Headers.Accept;

            var regex = new Regex(@"application\/vnd\.premieredigital\.v([\d]+)\+json", RegexOptions.IgnoreCase);

            foreach (var mime in acceptHeader)
            {
                Match match = regex.Match(mime.MediaType);
                if (match.Success == true)
                {
                    return match.Groups[1].Value; // change group selection based on regex if requried
                }
            }

            return this.GetLatestVersion(); // return latest version if not accept header provided (should be configured)
        }

        /// <summary>
        /// Gets the latest version.
        /// </summary>
        /// <returns>
        /// returns the latest version of API
        /// </returns>
        private string GetLatestVersion()
        {
            if (string.IsNullOrWhiteSpace(this.latestVersion))
            {
                IDictionary<string, HttpControllerDescriptor> controllerMappings = this.GetControllerMapping();

                if (controllerMappings != null && controllerMappings.Count > 0)
                {
                    ICollection<string> controllers = controllerMappings.Keys;

                    var regex = new Regex(@"^*[vV]([\d]+)$", RegexOptions.IgnoreCase);

                    List<int> versions = controllers.Select(c =>
                    {
                        Match match = regex.Match(c);
                        return match.Groups[1].Value;
                    }).Select(int.Parse).ToList();

                    this.latestVersion = versions.Max().ToString();
                }
            }

            return this.latestVersion;
        }
    }
}
