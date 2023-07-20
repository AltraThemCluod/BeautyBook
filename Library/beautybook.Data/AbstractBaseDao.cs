//-----------------------------------------------------------------------
// <copyright file="AbstractBaseDao.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Data
{    
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Common.Paging;
    using Dapper;

    /// <summary>
    /// Class Abstract Base Data Access Layer
    /// </summary>
    public abstract class AbstractBaseDao
    {
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <typeparam name="A">The abstract class.</typeparam>
        /// <typeparam name="I">The base model class.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>
        /// returns the paged list result.
        /// </returns>
        public async Task<SuccessResult<PagedList<A>>> ExecuteDynamicQuery<A, I>(string query, PageParam param)
            where A : BaseModel
            where I : BaseModel
        {
            SuccessResult<PagedList<A>> result = null;
            PagedList<A> page = new PagedList<A>(param);

            using (SqlConnection connection = new SqlConnection(Configurations.ConnectionString))
            {
                var task = await connection.QueryMultipleAsync(query);
                page.Values.AddRange(task.Read<I>().Select(s => s as A));
                page.TotalRecords = task.Read<int>().SingleOrDefault();
            }

            result = new SuccessResult<PagedList<A>>
            {
                Item = page,
                Code = 200
            };

            return result;
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>
        /// returns the paged list result.
        /// </returns>
        public async Task<SuccessResult<PagedList<dynamic>>> ExecuteDynamicQuery(string query, PageParam param)
        {
            SuccessResult<PagedList<dynamic>> result = null;
            PagedList<dynamic> page = new PagedList<dynamic>(param);

            using (SqlConnection connection = new SqlConnection(Configurations.ConnectionString))
            {
                var task = await connection.QueryMultipleAsync(query);
                page.Values.AddRange(task.Read<dynamic>());
                page.TotalRecords = task.Read<int>().SingleOrDefault();
            }

            result = new SuccessResult<PagedList<dynamic>>
            {
                Item = page,
                Code = 200
            };

            return result;
        }

        /// <summary>
        /// Strings the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>returns string object</returns>
        protected object String(string value)
        {
            return !string.IsNullOrWhiteSpace(value) ? value : (object)DBNull.Value;
        }

        /// <summary>
        /// Strings the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>returns string object</returns>
        protected object GetStringForArray(string[] value)
        {
            return value != null ? string.Join(",", value) : (object)DBNull.Value;
        }
    }
}
