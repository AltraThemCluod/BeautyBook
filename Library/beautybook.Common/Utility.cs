//-----------------------------------------------------------------------
// <copyright file="Utility.cs" company="">
//     Copyright . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;    
    using System.Net;
    using System.IO;

    /// <summary>
    /// Sealed Class Utility
    /// </summary>
    public static class Utility
    {
        #region Public Methods

        /// <summary>
        /// Creates the data table.
        /// </summary>
        /// <typeparam name="T">The class</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="EnzeoqueCon">The Enzeoque con.</param>
        /// <param name="startWith">The start with.</param>
        /// <returns>
        /// The data table
        /// </returns>
        public static DataTable CreateDataTable<T>(IEnumerable<T> list, List<string> EnzeoqueCon = null, string startWith = "")
        {
            try
            {
                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties().Where(p => p.CanRead).ToArray();

                DataTable dataTable = new DataTable();
                foreach (PropertyInfo info in properties)
                {
                    dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
                }

                if (EnzeoqueCon != null)
                {
                    DataColumn[] arrayDC = new DataColumn[EnzeoqueCon.Count];
                    for (int i = 0; i < EnzeoqueCon.Count; i++)
                    {
                        arrayDC[i] = dataTable.Columns[EnzeoqueCon[i]];
                    }

                   UniqueConstraint custEnzeoque = new UniqueConstraint(arrayDC);
                    dataTable.Constraints.Add(custEnzeoque);
                }

                foreach (T entity in list)
                {
                    object[] values = new object[properties.Count()];

                    for (int i = 0; i < properties.Count(); i++)
                    {
                        values[i] = properties[i].GetValue(entity);
                    }

                    try
                    {
                        dataTable.Rows.Add(values);
                    }
                    catch (Exception ex)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (Convert.ToString(values[i]).StartsWith(startWith))
                            {
                                //values[i] = GenerateHashIds(startWith);
                                break;
                            }
                        }

                        dataTable.Rows.Add(values);
                    }
                }

                return dataTable;
            }
            catch (Exception)
            {
                // todo Error Logging
                throw;
            }
        }

        public static string HtmlDecode(string htmlString)
        {
            return WebUtility.HtmlDecode(htmlString);
        }

        /// <summary>
        /// Gets the base path.
        /// </summary>
        /// <returns>the base path.</returns>
        public static string GetBasePath()
        {
            string root = string.Empty;

            if (string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath))
            {
                root = AppDomain.CurrentDomain.BaseDirectory; //// exe folder for WinForms, Consoles, Windows Services
            }
            else
            {
                root = AppDomain.CurrentDomain.RelativeSearchPath + "\\"; //// bin folder for Web Apps
            }

            return root;
        }

        /// <summary>
        /// Gets the cookie.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="key">The key.</param>
        /// <returns>returns cookie value.</returns>
        //public static dynamic GetCookie(HttpRequestMessage request, string key)
        //{
        //    dynamic cookieValue = null;
        //    var cookie = request.Headers.GetCookies().SelectMany(c => c.Cookies).FirstOrDefault(c => c.Name.Equals(key, StringComparison.InvariantCulture));
        //    if (cookie != null)
        //    {
        //        if (!string.IsNullOrWhiteSpace(cookie.Value))
        //        {
        //            cookieValue = cookie.Value;
        //        }
        //        else if (cookie.Values != null && cookie.Values.Count > 0)
        //        {
        //            cookieValue = cookie.Values;
        //        }
        //    }

        //    return cookieValue;
        //}

        /// <summary>
        /// Returns an individual HTTP Header value
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="key">The key.</param>
        /// <returns>returns header value</returns>
        public static string GetHeader(HttpRequestMessage request, string key)
        {
            IEnumerable<string> keys = null;
            if (!request.Headers.TryGetValues(key, out keys))
            {
                return null;
            }

            return keys.First();
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="entity">The source.</param>
        /// <param name="propName">Name of the property.</param>
        /// <returns>Returns the object.</returns>
        public static object GetPropValue(object entity, string propName)
        {
            return entity.GetType().GetProperty(propName).GetValue(entity, null);
        }

        #endregion

        public static bool Deletefile(string Filepath)
        {
            System.IO.File.Delete(Filepath);
            return true;
        }

        public static bool Deletefolder(string avatarfolder)
        {
            System.IO.Directory.Delete(avatarfolder);
            return true;
        }
    }
}