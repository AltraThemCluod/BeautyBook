//-----------------------------------------------------------------------
// <copyright file="Authenticate.cs" company="Premiere Digital Services">
//     Copyright Premiere Digital Services. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBookApi
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;    
    using System.Threading.Tasks;
    using System.Threading;
    using System.Collections.Generic;
    using BeautyBook.Common;

    /// <summary>
    /// Custom Authentication Filter Extending basic Authentication
    /// </summary>
    public class Authenticate : AuthorizationFilterAttribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Authenticate"/> class.
        /// </summary>
        public Authenticate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authenticate"/> class.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        public Authenticate(bool isActive)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Called when [authorization asynchronous].
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async override Task OnAuthorizationAsync(HttpActionContext filterContext, CancellationToken cancellationToken)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes<System.Web.Http.AllowAnonymousAttribute>(true).Any())
            {
                await base.OnAuthorizationAsync(filterContext, cancellationToken);
            }
            else
            {
                SuccessResult<string> result = new SuccessResult<string>();
                result.Code = 401;
                result.Message = "Please contact system administrator of KeViMart.";
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized,result);
                //string result = await this.ValidateAuthToken(filterContext);

                //if (result != string.Empty)
                //{
                //    filterContext.Request.Properties[Constant.AUTHVALIDATEDUSERHEADERKEY] = result;

                //    ////System.Web.HttpCookie cookie = new System.Web.HttpCookie(Constant.AUTHXAUTHTOKEN, user.Item.GetToken()) { HttpOnly = true, Domain = Config.CookieDomain, Secure = Config.IsSecureCookie, Path = "/" };
                //    ////System.Web.HttpContext.Current.Response.SetCookie(cookie);

                //    await base.OnAuthorizationAsync(filterContext, cancellationToken);
                //}
                //else
                //{
                //    filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                //}
            }
        }

        /*
         * https://msdn.microsoft.com/en-us/magazine/gg598924.aspx
         * NEVER USE ASYNC VOID
         */
        /////// <summary>
        /////// Checks basic authentication request
        /////// </summary>
        /////// <param name="filterContext">The filter context.</param>
        ////public async override void OnAuthorization(HttpActionContext filterContext)
        ////{
        ////    if (filterContext.ActionDescriptor.GetCustomAttributes<System.Web.Http.AllowAnonymousAttribute>(true).Any())
        ////    {
        ////        return;
        ////    }

        ////    string result = await this.ValidateAuthToken(filterContext);

        ////    if (result != string.Empty)
        ////    {
        ////        filterContext.Request.Properties[Constant.AUTHVALIDATEDUSERHEADERKEY] = result;
        ////        base.OnAuthorization(filterContext);
        ////    }
        ////    else
        ////    {
        ////        filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        ////    }
        ////}

        #endregion Public Methods

        #region Private Methods

        //protected virtual async Task<string> ValidateAuthToken(HttpActionContext filterContext)
        //{
        //    string result = string.Empty;

        //    string pdxApiKey = Utility.GetHeader(filterContext.Request, Constant.AUTHXPDXAPIKEY);
        //    string pdxToken = Utility.GetCookie(filterContext.Request, Constant.AUTHXAUTHTOKEN);
        //    string pdxTokenTime = Utility.GetCookie(filterContext.Request, Constant.AUTHXAUTHTOKENTIME);

        //    if (!(string.IsNullOrWhiteSpace(pdxToken) && string.IsNullOrWhiteSpace(pdxApiKey)))
        //    {
        //        string uri = System.IO.Path.Combine(BaseConfigurations.AuthServiceURL, string.Format("Users/Validate?token={0}&apiKey={1}&tokenTime={2}", pdxToken, pdxApiKey, pdxTokenTime));

        //        var cookieContainer = new CookieContainer();
        //        HttpClientHandler handler = new HttpClientHandler() { CookieContainer = cookieContainer };
        //        var client = new HttpClient(handler);
        //        client.BaseAddress = new Uri(uri);

        //        HttpResponseMessage response = client.PostAsJsonAsync(uri, filterContext.Request.Content).Result;
        //        string newToken = string.Empty, newTokenTime = string.Empty;

        //        IEnumerable<Cookie> responseCookies = cookieContainer.GetCookies(new Uri(uri)).Cast<Cookie>();
        //        foreach (Cookie cookie in responseCookies)
        //        {
        //            if (cookie.Name == Constant.AUTHXAUTHTOKEN)
        //            {
        //                newToken = cookie.Value + "@" + cookie.Expires;
        //                GlobalLogger.Current.Info(cookie.Name + ": " + cookie.Value);
        //            }

        //            if (cookie.Name == Constant.AUTHXAUTHTOKENTIME)
        //            {
        //                newTokenTime = cookie.Value + "@" + cookie.Expires;
        //                GlobalLogger.Current.Info(cookie.Name + ": " + cookie.Value);
        //            }
        //        }

        //        if (pdxToken != newToken && newToken != string.Empty)
        //        {
        //            try
        //            {
        //                filterContext.Request.Properties[Constant.AUTHXAUTHTOKEN] = newToken;
        //                filterContext.Request.Properties[Constant.AUTHXAUTHTOKENTIME] = newTokenTime;
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }

        //        if (response.IsSuccessStatusCode)
        //        {
        //            result = await response.Content.ReadAsStringAsync();
        //        }
        //    }

        //    return result;
        //}

        #endregion Private Methods
    }
}