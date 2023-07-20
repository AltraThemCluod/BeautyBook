//-----------------------------------------------------------------------
// <copyright file="CustomNegotiatedContentResult.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Results;

    /// <summary>
    /// Represents an action result that performs content negotiation.
    /// </summary>
    /// <typeparam name="T">The type of content in the entity body.</typeparam>
    public class CustomNegotiatedContentResult<T> : NegotiatedContentResult<T>
    {
        /// <summary>
        /// The location
        /// </summary>
        private string location;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomNegotiatedContentResult{T}"/> class with the values provided.
        /// </summary>
        /// <param name="statusCode">The HTTP status code for the response message.</param>
        /// <param name="content">The content value to negotiate and format in the entity body.</param>
        /// <param name="controller">The controller from which to obtain the dependencies needed for execution.</param>
        public CustomNegotiatedContentResult(HttpStatusCode statusCode, T content, ApiController controller)
            : base(statusCode, content, controller)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomNegotiatedContentResult{T}" /> class with the values provided.
        /// </summary>
        /// <param name="statusCode">The HTTP status code for the response message.</param>
        /// <param name="content">The content value to negotiate and format in the entity body.</param>
        /// <param name="location">The location.</param>
        /// <param name="controller">The controller from which to obtain the dependencies needed for execution.</param>
        public CustomNegotiatedContentResult(HttpStatusCode statusCode, T content, string location, ApiController controller)
            : this(statusCode, content, controller)
        {
            this.location = location;
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location
        {
            get
            {
                return this.location;
            }
        }

        /// <inheritdoc />
        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.ExecuteAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(this.Location))
            {
                response.Headers.Location = new Uri(this.Location, UriKind.RelativeOrAbsolute);
            }

            return response;
        }
    }
}
