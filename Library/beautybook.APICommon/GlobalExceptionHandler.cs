//-----------------------------------------------------------------------
// <copyright file="GlobalExceptionHandler.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{    
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;
    using Common;

    /// <summary>
    /// Global un handled exception handler
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler 
    {
        /// <summary>
        /// The error message
        /// </summary>
        private const string ErrorMessage = "An unexpected error has occurred.";

        /// <summary>
        /// When overridden in a derived class, handles the exception asynchronously.
        /// </summary>
        /// <param name="context">The exception handler context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous exception handling operation.
        /// </returns>
        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            context.Result = new JsonErrorResult<ErrorResult>
            {
                Request = context.Request,
                Content = GetErrorResult(ErrorMessage)
            };

            await base.HandleAsync(context, cancellationToken);
        }

        /// <summary>
        /// Gets the error result.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// The error result.
        /// </returns>
        private static ErrorResult GetErrorResult(string message)
        {
            ErrorResult result = new ErrorResult();
            result.Error.Message = message;
            return result;
        }

        /// <summary>
        /// Custom JSON Error Result.
        /// </summary>
        /// <typeparam name="T">The class.</typeparam>
        /// <seealso cref="System.Web.Http.IHttpActionResult" />
        private class JsonErrorResult<T> : IHttpActionResult where T : class
        {
            /// <summary>
            /// Gets or sets the request.
            /// </summary>
            /// <value>
            /// The request.
            /// </value>
            public HttpRequestMessage Request { get; set; }

            /// <summary>
            /// Gets or sets the content.
            /// </summary>
            /// <value>
            /// The content.
            /// </value>
            public T Content { get; set; }

            /// <summary>
            /// Creates an <see cref="T:System.Net.Http.HttpResponseMessage" /> asynchronously.
            /// </summary>
            /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
            /// <returns>
            /// A task that, when completed, contains the <see cref="T:System.Net.Http.HttpResponseMessage" />.
            /// </returns>
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response =
                                 new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new ObjectContent<T>(this.Content, new JsonMediaTypeFormatter());
                response.RequestMessage = this.Request;
                return Task.FromResult(response);
            }
        }
    }
}
