//-----------------------------------------------------------------------
// <copyright file="GlobalMessageLogger.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{   
    using System.Threading.Tasks;
    using BeautyBook.Common;

    /// <summary>
    /// Class GlobalMessageLogger
    /// </summary>
    public class GlobalMessageLogger : GlobalMessageHandler
    {
        /// <summary>
        /// Request the message asynchronous.
        /// </summary>
        /// <param name="infoLog">The message model.</param>
        /// <returns>
        /// Request Message
        /// </returns>
        protected override async Task RequestMessageAsync(InfoLog infoLog)
        {
            await GlobalLogger.Current.Info(infoLog.GetRequestLog());            
        }

        /// <summary>
        /// Response the message asynchronous.
        /// </summary>
        /// <param name="infoLog">The message model.</param>
        /// <returns>
        /// Response Message
        /// </returns>
        protected override async Task ResponseMessageAsync(InfoLog infoLog)
        {
            await GlobalLogger.Current.Info(infoLog.GetResponseLog());            
        }
    }
}
