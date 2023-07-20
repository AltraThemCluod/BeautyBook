//-----------------------------------------------------------------------
// <copyright file="QueryParam.cs" company="">
//     Copyright . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------



namespace BeautyBook.Common
{
    using BeautyBook.Common.Paging;
    /// <summary>
    /// Query Parameter
    /// </summary>
    public sealed class QueryParam : PageParam
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParam"/> class.
        /// </summary>
        public QueryParam()
        {
        }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the aggregate.
        /// </summary>
        /// <value>
        /// The aggregate.
        /// </value>
        public string Aggregate { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public string Sort { get; set; }
    }
}
