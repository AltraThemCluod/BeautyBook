//-----------------------------------------------------------------------
// <copyright file="ErrorResult.cs" company="">
//     Copyright . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///  Class ErrorResult
    /// </summary>
    public class ErrorResult : AbstractResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResult"/> class.
        /// </summary>
        public ErrorResult()
        {
            this.Error = new Error();
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public Error Error { get; set; }
    }

    /// <summary>
    /// Class Error
    /// </summary>
    public class Error
    {
        ///// <summary>
        ///// Gets or sets the code.
        ///// </summary>
        ///// <value>
        ///// The Code.
        ///// </value>
        //// public string Code { get; set; }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        /// <value>
        /// The Message.
        /// </value>
        public string Message { get; set; }
    }
}
