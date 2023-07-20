//-----------------------------------------------------------------------
// <copyright file="InfoLog.cs" company="">
//     Copyright . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Common
{
    /// <summary>
    /// Class InfoLog
    /// </summary>
    public class InfoLog
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the version of API implementation (i.e. V1, V2 etc.).
        /// </summary>
        /// <value>The version of API implementation (i.e. V1, V2 etc.).</value>
        public string APIVersion { get; set; }

        /// <summary>
        /// Gets or sets the Correlation identifier.
        /// </summary>
        /// <value>The correlation identifier.</value>
        public string CorrelationId { private get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        /// <value>
        /// The IP address.
        /// </value>
        public string IpAddress { private get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { private get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public string Method { private get; set; }

        /// <summary>
        /// Gets or sets the phrase.
        /// </summary>
        /// <value>The phrase.</value>
        public string Phrase { private get; set; }

        /// <summary>
        /// Gets or sets the processing time in Milliseconds.
        /// </summary>
        /// <value>The processing time milliseconds.</value>
        public long ProcessingTimeMS { get; set; }

        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        /// <value>The request URL.</value>
        public string RequestUrl { private get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the request log.
        /// </summary>
        /// <returns>Request log</returns>
        public string GetRequestLog()
        {
            return string.Format(
                "CorrelationId: {0} - Request: {1} - V{4} - {2} {3} IpAddress: {5}",
                this.CorrelationId,
                this.Method,
                this.RequestUrl,
                !string.IsNullOrWhiteSpace(this.Message) ? string.Concat("\r\n", this.Message) : string.Empty,
                this.APIVersion,
                this.IpAddress);
        }

        /// <summary>
        /// Gets the response log.
        /// </summary>
        /// <returns>Response log</returns>
        public string GetResponseLog()
        {
            return string.Format("CorrelationId: {0} ({3} milliseconds) - Response: {1} {2}", this.CorrelationId, this.Phrase, this.Message, this.ProcessingTimeMS);
        }

        #endregion Public Methods
    }
}