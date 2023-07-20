//-----------------------------------------------------------------------
// <copyright file="WebApiActionFilter.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{    
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Filters;
    using BeautyBook.Common;

    /// <summary>
    /// Web API Action Filter
    /// </summary>
    public class WebApiActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Called when [action executed asynchronous].
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>action executed</returns>
        ////public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        ////{
        ////    this.SetResponseCookie(actionExecutedContext);

        ////    await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        ////}

        /// <summary>
        /// Sets the response cookie.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        ////private void SetResponseCookie(HttpActionExecutedContext actionExecutedContext)
        ////{
        ////    try
        ////    {
        ////        var newToken = actionExecutedContext.Request.Properties.ContainsKey(Constant.AUTHTOKEN) ? actionExecutedContext.Request.Properties[Constant.AUTHTOKEN].ToString() : string.Empty;
        ////        var newTokenTime = actionExecutedContext.Request.Properties.ContainsKey(Constant.TOKENTIME) ? actionExecutedContext.Request.Properties[Constant.TOKENTIME].ToString() : string.Empty;

        ////        if (newToken != string.Empty)
        ////        {
        ////            HttpCookie newTokenCookie = new HttpCookie(Constant.AUTHTOKEN, newToken.Split('@')[0]) { HttpOnly = true, Domain = Configurations.CookieDomain, Secure = false, Path = "/" };
        ////            if (newToken.Contains("@"))
        ////            {
        ////                newTokenCookie.Expires = Convert.ToDateTime(newToken.Split('@')[1]);
        ////            }

        ////            //// HttpCookie newTokenTimeCookie = new HttpCookie(Constant.TOKENTIME, (DateTime.UtcNow.AddSeconds(60) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds.ToString()) { HttpOnly = true, Domain = BaseConfigurations.CookieDomain, Secure = false, Path = "/", Expires = DateTime.UtcNow.AddSeconds(60) };
        ////            HttpCookie newTokenTimeCookie = new HttpCookie(Constant.TOKENTIME, (DateTime.UtcNow.AddSeconds(Convert.ToInt32(newTokenTime.Split('@')[0])) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds.ToString()) { HttpOnly = true, Domain = Configurations.CookieDomain, Secure = false, Path = "/", Expires = Convert.ToDateTime(newTokenTime.Split('@')[1]) };
        ////            HttpContext.Current.Response.SetCookie(newTokenCookie);
        ////            HttpContext.Current.Response.SetCookie(newTokenTimeCookie);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ////GlobalLogger.Current.Error(ex);
        ////    }
        ////}
    }
}
