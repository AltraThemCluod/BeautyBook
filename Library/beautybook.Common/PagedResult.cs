//-----------------------------------------------------------------------
// <copyright file="PagedResult.cs" company="">
//     Copyright . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Common
{    
    using System.Collections.Generic;
    using BeautyBook.Common;
    using BeautyBook.Common.Paging;

    /// <summary>
    /// Class page result
    /// </summary>
    /// <typeparam name="T">The Base Model Type.</typeparam>
    public class PagedResult<T> : SuccessListResult<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
        /// </summary>
        public PagedResult() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public PagedResult(PagedList<T> result) : base(result.Values)
        {
            this._links = new Dictionary<string, string>();
            this.TotalResults = result.TotalRecords;
        }

        /// <summary>
        /// Gets or sets the _links.
        /// </summary>
        /// <value>
        /// The _links.
        /// </value>
        public IDictionary<string, string> _links { get; set; }

        /// <summary>
        /// Gets or sets the total results.
        /// </summary>
        /// <value>
        /// The total results.
        /// </value>
        public long TotalResults { get; set; }
    }
}
