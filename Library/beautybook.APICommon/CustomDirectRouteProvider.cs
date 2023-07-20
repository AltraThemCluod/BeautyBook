//-----------------------------------------------------------------------
// <copyright file="CustomDirectRouteProvider.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Routing;

    /// <summary>
    /// Custom direct route provider
    /// </summary>
    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        /// <summary>
        /// Gets a set of route factories for the given action descriptor.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>
        /// A set of route factories.
        /// </returns>
        protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            // Get all the route attributes decorated directly on the actions
            IReadOnlyList<IDirectRouteFactory> actionRouteFactories = actionDescriptor.GetCustomAttributes<InheritedRouteAttribute>(inherit: true);

            // Check if the route attribute on each action already has a route name and if no, 
            // generate a route name automatically
            // based on the convention: _ (ex: Customers_GetById)
            foreach (IDirectRouteFactory routeFactory in actionRouteFactories)
            {
                InheritedRouteAttribute routeAttr = routeFactory as InheritedRouteAttribute;

                Type t = actionDescriptor.ControllerDescriptor.ControllerType;

                var segments = t.Namespace.Split(Type.Delimiter);

                // For the dictionary key, strip "Controller" from the end of the type name.
                // This matches the behavior of DefaultHttpControllerSelector.
                var version = segments[segments.Length - 1];

                routeAttr.Name = version + actionDescriptor.ActionName;
            }

            return actionRouteFactories;
        }

        /// <summary>
        /// Gets the route prefix from the provided controller.
        /// </summary>
        /// <param name="controllerDescriptor">The controller descriptor.</param>
        /// <returns>
        /// The route prefix or null.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot support multiple route prefix -  + controllerDescriptor.ControllerType.FullName
        /// or
        /// Prefix cannot be null -  + controllerDescriptor.ControllerType.FullName
        /// or
        /// Invalid Prefix -  + controllerDescriptor.ControllerType.FullName
        /// </exception>
        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            ICollection<IRoutePrefix> attributes = controllerDescriptor.GetCustomAttributes<IRoutePrefix>(inherit: true);

            if (attributes == null)
            {
                return null;
            }

            if (attributes.Count > 1)
            {
                throw new InvalidOperationException("Cannot support multiple routeprefix - " + controllerDescriptor.ControllerType.FullName);
            }

            if (attributes.Count == 1)
            {
                IRoutePrefix attribute = attributes.FirstOrDefault();

                if (attribute != null)
                {
                    string prefix = attribute.Prefix;

                    if (prefix == null)
                    {
                        throw new InvalidOperationException("Prefix cannot be null - " + controllerDescriptor.ControllerType.FullName);
                    }

                    if (prefix.EndsWith("/", StringComparison.Ordinal))
                    {
                        throw new InvalidOperationException("Invalid Prefix - " + controllerDescriptor.ControllerType.FullName);
                    }

                    return GetRoutePrefixForController(prefix, controllerDescriptor.ControllerName);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the route prefix for controller.
        /// </summary>
        /// <param name="routePrefix">The route prefix.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns>dynamic route prefix.</returns>
        private static string GetRoutePrefixForController(string routePrefix, string controllerName)
        {
            string controller = Regex.Replace(controllerName, @"^*[vV]([\d]+)$", string.Empty);

            return Regex.Replace(routePrefix, @"\[[cC]ontroller\]", ConvertToCamelCase(controller));
        }

        /// <summary>
        /// Converts to camel case.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        /// Convert string to camel case
        /// </returns>
        private static string ConvertToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            char[] a = s.ToCharArray();
            a[0] = char.ToLower(a[0]);

            return new string(a);
        }
    }
}
