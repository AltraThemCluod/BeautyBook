//-----------------------------------------------------------------------
// <copyright file="GlobalExceptionLogger.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{    
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.ExceptionHandling;
    using BeautyBook.Common;

    /// <summary>
    /// Represents an unhandled exception logger.
    /// </summary>
    public class GlobalExceptionLogger : ExceptionLogger
    {
        /// <summary>
        /// When overridden in a derived class, logs the exception asynchronously.
        /// </summary>
        /// <param name="context">The exception logger context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task representing the asynchronous exception logging operation.
        /// </returns>
        public async override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            Exception ex = context.Exception;

            GlobalLogger.AddVariable("CorrelationId", context.Request.GetCorrelationId());            

            await GlobalLogger.Current.Error(ex);

            await base.LogAsync(context, cancellationToken);
        }
    }
}
