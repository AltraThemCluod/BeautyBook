//-----------------------------------------------------------------------
// <copyright file="PagedList.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PublicZaanVendor.Common.Paging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class paged list
    /// </summary>
    /// <typeparam name="T">The Base Model Type.</typeparam>
    public class PagedList<T> : PageParam where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        public PagedList()
        {
            this.Values = new List<T>();
        }

        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        /// <param name="pageParam">The page parameter.</param>
        public PagedList(PageParam pageParam) : base(pageParam)
        {
            this.Values = new List<T>();
        }       

        /// <summary>
        /// Gets or sets the total records.
        /// </summary>
        /// <value>
        /// The total records.
        /// </value>
        public long TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public List<T> Values { get; set; }
    }
}
