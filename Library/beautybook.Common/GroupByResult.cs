//-----------------------------------------------------------------------
// <copyright file="GroupByResult.cs" company="">
//     Copyright . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Common
{
    /// <summary>
    /// Class success result
    /// </summary>
    /// <typeparam name="T">Item class</typeparam>
    public class GroupByResult<T> where T : class
    {
        /// <summary>
        /// Gets the total results.
        /// </summary>
        /// <value>
        /// The total results.
        /// </value>
        public int TotalResults
        {
            get
            {
                int total = 0;
                foreach (dynamic item in (dynamic)this.Aggregates)
                {
                    total += item.Total;
                }

                return total;
            }
        }

        /// <summary>
        /// Gets or sets the Aggregates.
        /// </summary>
        /// <value>
        /// The Aggregates.
        /// </value>
        public T Aggregates { get; set; }
    }
}
