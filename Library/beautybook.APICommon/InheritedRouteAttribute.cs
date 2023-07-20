//-----------------------------------------------------------------------
// <copyright file="InheritedRouteAttribute.cs" company="Premiere Digital Services">
//     Copyright Premiere Digital Services. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Http.Routing;

    /// <summary>
    /// Custom route
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class InheritedRouteAttribute : Attribute, IDirectRouteFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InheritedRouteAttribute"/> class.
        /// </summary>
        /// <param name="template">The template.</param>
        public InheritedRouteAttribute(string template)
        {
            this.Template = template;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }

        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        public string Template { get; private set; }

        /// <summary>
        /// Creates a direct route entry.
        /// </summary>
        /// <param name="context">The context to use to create the route.</param>
        /// <returns>
        /// The direct route entry.
        /// </returns>
        public RouteEntry CreateRoute(DirectRouteFactoryContext context)
        {
            // context.Actions will always contain at least one action - and all of the 
            // actions will always belong to the same controller.
            IDirectRouteBuilder builder = context.CreateBuilder(this.Template);
            builder.Name = this.Name;
            builder.Order = this.Order;
            return builder.Build();
        }
    }
}
