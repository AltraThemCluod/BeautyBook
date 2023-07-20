//-----------------------------------------------------------------------
// <copyright file="LoggerDelegatingHandler.cs" company="Premiere Digital Services">
//     Copyright Premiere Digital Services. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PDX.Common.API
{
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Logger handler
    /// </summary>
    public class LoggerDelegatingHandler : DelegatingHandler
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly Logger Log = LogManager.GetLogger("infoLogger");        

        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>
        /// Returns <see cref="T:System.Threading.Tasks.Task`1" />. The task object representing the asynchronous operation.
        /// </returns>
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();

            Guid guid = Guid.NewGuid();
            LogManager.Configuration.Variables["Requestid"] = guid.ToString();
            NLog.Contrib.MappedDiagnosticsLogicalContext.Set("requestid", guid.ToString());
            stopwatch.Start();
            
            Log.Trace("RequestId={0} Method=[{1}] RequestedUrl={2}", guid.ToString(), request.Method.Method, request.RequestUri);
            
            var response = await base.SendAsync(request, cancellationToken);
            
            string result = await response.Content.ReadAsStringAsync();

            stopwatch.Stop();

            Log.Trace("RequestId={0} Duration={1} StatusCode={2} ResponseData={3}", guid.ToString(), (object)stopwatch.Elapsed, (object)response.StatusCode, result);
            
            return response;
        }
    }
}
