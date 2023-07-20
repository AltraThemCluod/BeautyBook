//-----------------------------------------------------------------------
// <copyright file="SuccessResult.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PublicZaanVendor.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class success result
    /// </summary>
    /// <typeparam name="T">Item class</typeparam>
    public class SuccessResult<T> where T : class
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public T Item { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                IEnumerable<T> enumerable = this.Item as IEnumerable<T>;

                if (enumerable != null && enumerable.Any())
                {
                    return true;
                }

                return this.Item != null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is success status code.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is success status code; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessStatusCode
        {
            get
            {
                return this.Code >= 200 && this.Code <= 299;
            }
        }
    }
}
