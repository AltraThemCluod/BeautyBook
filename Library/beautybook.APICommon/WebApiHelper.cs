//-----------------------------------------------------------------------
// <copyright file="WebApiHelper.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{    
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using BeautyBook.Common;

    /// <summary>
    /// Class Web API Helper
    /// </summary>
    public class WebApiHelper
    {
        /// <summary>
        /// Reaches the HTTP client request response.
        /// </summary>
        /// <typeparam name="REQ">The type of the EQ.</typeparam>
        /// <typeparam name="RES">The type of the ES.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="methodType">Type of the method.</param>
        /// <param name="requestContent">Content of the request.</param>
        /// <param name="defaultRequestHeaders">The default request headers.</param>
        /// <returns>
        /// Return Meta data Object
        /// </returns>
        ////public static async Task<RES> HttpClientRequestReponce<REQ, RES>(string uri, HttpMethod methodType, REQ requestContent, NameValueCollection defaultRequestHeaders = null)
        ////{
        ////    HttpClient client = null;

        ////    var cookieContainer = GetAuthCookieCollection();

        ////    HttpClientHandler handler = new HttpClientHandler() { CookieContainer = cookieContainer };

        ////    client = new HttpClient(handler);

        ////    client.DefaultRequestHeaders.Accept.Clear();

        ////    AddHeaders(client, defaultRequestHeaders);

        ////    HttpResponseMessage response;

        ////    if (methodType == System.Net.Http.HttpMethod.Get)
        ////    {
        ////        response = await client.GetAsync(uri);
        ////        if (response.IsSuccessStatusCode)
        ////        {
        ////            var result = await response.Content.ReadAsAsync<RES>();
        ////            return result;
        ////        }
        ////    }
        ////    else if (methodType == System.Net.Http.HttpMethod.Post)
        ////    {
        ////        response = await client.PostAsJsonAsync(uri, requestContent);

        ////        if (response.IsSuccessStatusCode)
        ////        {
        ////            var result = await response.Content.ReadAsAsync<RES>();
        ////            return result;
        ////        }
        ////    }
        ////    else if (methodType == System.Net.Http.HttpMethod.Put)
        ////    {
        ////        response = await client.PutAsync(uri, requestContent as HttpContent);
        ////        if (response.IsSuccessStatusCode)
        ////        {
        ////            var result = await response.Content.ReadAsAsync<RES>();
        ////            return result;
        ////        }
        ////    }

        ////    return default(RES);
        ////}

        /// <summary>
        /// Reaches the HTTP client request response.
        /// </summary>
        /// <typeparam name="RES">The type of the ES.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="methodType">Type of the method.</param>
        /// <param name="requestContent">Content of the request.</param>
        /// <param name="defaultRequestHeaders">The default request headers.</param>
        /// <returns>
        /// Return Meta data Object
        /// </returns>
        public static async Task<RES> HttpClientPostRequestReponce<RES>(string uri, HttpMethod methodType, HttpContent requestContent, NameValueCollection defaultRequestHeaders = null)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();

            AddHeaders(client, defaultRequestHeaders);

            HttpResponseMessage response;

            if (methodType == System.Net.Http.HttpMethod.Post)
            {
                response = await client.PostAsync(uri, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<RES>();
                    if (uri.ToLower().Contains("feddev.premieredigital.net"))
                    {
                        Exception pingEx = new Exception("Success Ping: " + response.ToString());
                        GlobalLogger.Current.Error(pingEx);
                    }

                    return result;
                }
                else if (uri.ToLower().Contains("feddev.premieredigital.net"))
                {
                    Exception pingEx = new Exception(response.ToString());
                    GlobalLogger.Current.Error(pingEx);
                }
            }

            return default(RES);
        }

        /// <summary>
        /// Sets the application variable.
        /// </summary>
        /// <param name="appData">The application data.</param>
        /// <param name="token">The token.</param>
        /// <param name="user">The user.</param>
        /// <param name="oldToken">The old token.</param>
        /// <returns>
        /// Dictionary Object to set in Application variable
        /// </returns>
        public static object SetApplicationVar(object appData, string token, object user, string oldToken = "")
        {
            Dictionary<string, object> applicationNewData = new Dictionary<string, object>();
            if (appData != null)
            {
                applicationNewData = (Dictionary<string, object>)appData;
            }

            applicationNewData.Add(token, user);

            //////// Code to remove old Token from Application var
            ////if (oldToken != string.Empty)
            ////{
            ////    foreach (string key in applicationNewData.Keys)
            ////    {
            ////        if (key.StartsWith(oldToken))
            ////        {
            ////            applicationNewData.Remove(key);
            ////            break;
            ////        }
            ////    }
            ////}
            //////// 
            return applicationNewData;
        }

        /// <summary>
        /// Gets the authentication cookie collection.
        /// </summary>
        /// <returns> Cookie Container </returns>
        ////private static CookieContainer GetAuthCookieCollection()
        ////{
        ////    CookieContainer cookieContainer = new CookieContainer();

        ////    foreach (string key in HttpContext.Current.Request.Cookies.AllKeys)
        ////    {
        ////        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);

        ////        if (cookie != null)
        ////        {
        ////            cookieContainer.Add(new Cookie(cookie.Name, cookie.Value) { Domain = Configurations.CookieDomain, HttpOnly = true, Secure = false, Path = "/" });
        ////        }
        ////    }            

        ////    return cookieContainer;
        ////}

        /// <summary>
        /// Adds the headers.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="defaultRequestHeaders">The default request headers.</param>
        private static void AddHeaders(HttpClient httpClient, NameValueCollection defaultRequestHeaders)
        {
            if (defaultRequestHeaders != null)
            {
                foreach (var key in defaultRequestHeaders.AllKeys)
                {
                    if (!WebHeaderCollection.IsRestricted(key))
                    {
                        httpClient.DefaultRequestHeaders.Add(key, defaultRequestHeaders[key]);

                        GlobalLogger.Current.Info("Order service Header set Key:" + key + "Value:" + defaultRequestHeaders[key]);
                    }

                    if (key == "Accept")
                    {
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(defaultRequestHeaders[key]));
                    }
                }
            }

            if (HttpContext.Current.Request.Headers != null)
            {
                foreach (var key in HttpContext.Current.Request.Headers.AllKeys)
                {
                    if (!WebHeaderCollection.IsRestricted(key))
                    {
                        httpClient.DefaultRequestHeaders.Add(key, HttpContext.Current.Request.Headers[key]);
                        GlobalLogger.Current.Info("Order service Header set Key:" + key + "Value:" + HttpContext.Current.Request.Headers[key]);
                    }
                }
            }
        }
    }
}
