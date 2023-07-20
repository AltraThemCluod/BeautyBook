//-----------------------------------------------------------------------
// <copyright file="GlobalLogger.cs" company="">
//     Copyright . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Common
{
    using System;
    using System.Threading.Tasks;
    using NLog;

    /// <summary>
    /// Default logger
    /// </summary>
    public class GlobalLogger
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static readonly GlobalLogger Instance;

        /// <summary>
        /// The logger.
        /// </summary>
        private Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes static members of the <see cref="GlobalLogger"/> class.
        /// </summary>
        static GlobalLogger()
        {
            Instance = new GlobalLogger();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="GlobalLogger"/> class from being created.
        /// </summary>
        private GlobalLogger()
        {
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public static GlobalLogger Current
        {
            get
            {
                return Instance;
            }
        }

        /// <summary>
        /// Adds the variable.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddVariable(string key, object value)
        {
            LogManager.Configuration.Variables[key] = value.ToString();
        }

        /// <summary>
        /// Information level log.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <returns>The Task</returns>
        public async Task Info(string log)
        {
            await Task.Run(() =>
            {
                this.logger.Info<string>(log);
            });
        }
        
        /// <summary>
        /// Errors the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>The Task</returns>
        public async Task Error(Exception ex)
        {
            await Task.Run(() =>
            {
                this.logger.Error(ex);
            });
        }
    }
}
