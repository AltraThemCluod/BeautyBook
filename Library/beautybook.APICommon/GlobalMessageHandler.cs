//-----------------------------------------------------------------------
// <copyright file="GlobalMessageHandler.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{   
    using System.Net.Http;
    using System.ServiceModel.Channels;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using BeautyBook.Common;

    /// <summary>
    /// Contract Class GlobalMessageHandler
    /// </summary>
    public abstract class GlobalMessageHandler : DelegatingHandler
    {
        /// <summary>
        /// The HTTP context
        /// </summary>
        private const string HttpContext = "MS_HttpContext";

        #region Protected Methods

        /// <summary>
        /// Request the message asynchronous.
        /// </summary>
        /// <param name="infoLog">The message model.</param>
        /// <returns>Request Message</returns>
        protected abstract Task RequestMessageAsync(InfoLog infoLog);

        /// <summary>
        /// Response the message asynchronous.
        /// </summary>
        /// <param name="infoLog">The information log.</param>
        /// <returns>Response Message</returns>
        protected abstract Task ResponseMessageAsync(InfoLog infoLog);

        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1"/>. The task object representing the
        /// asynchronous operation.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            InfoLog infoLog = new InfoLog();

            infoLog.CorrelationId = request.GetCorrelationId().ToString();
            infoLog.Method = request.Method.ToString();
            infoLog.RequestUrl = request.RequestUri.ToString();
            infoLog.Message = await request.Content.ReadAsStringAsync();
            infoLog.APIVersion = this.GetVersion(request);
            infoLog.IpAddress = this.GetIPAddress(request);

            await this.RequestMessageAsync(infoLog);

            //// Start tracking execution time
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            var response = await base.SendAsync(request, cancellationToken);

            //// Stop tracking and get total execution time
            sw.Stop();
            infoLog.ProcessingTimeMS = sw.ElapsedMilliseconds;

            infoLog.Phrase = response.ReasonPhrase;
            infoLog.Message = response.IsSuccessStatusCode && response.Content != null ? await response.Content.ReadAsStringAsync() : string.Empty;

            await this.ResponseMessageAsync(infoLog);

            return response;
        }

        #endregion Protected Methods

        #region Private Methods

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

            return "(default)"; // return zero version if not accept header provided (should be configured), denotes no versioning specified
        }

        /// <summary>
        /// Gets the IP address.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// IP Address.
        /// </returns>
        private string GetIPAddress(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey(HttpContext))
            {
                return ((HttpContextWrapper)request.Properties[HttpContext]).Request.UserHostAddress;
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                if (prop != null)
                {
                    return prop.Address;
                }
            }

            return null;
        }

        #endregion Private Methods
    }
}