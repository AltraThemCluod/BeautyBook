using BeautyBook.Common;
using System;
using System.Configuration;
using System.Web;

namespace BeautyBookAdmin.Infrastructure
{
    public class ErrorLogHelper
    {
        #region Properties

        /// <summary>
        /// The _error log repository
        /// </summary>
        //private static ErrorLogService _errorLogService;

        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLog"/> class.
        /// </summary>
        //public ErrorLogHelper()
        //{
        //    _errorLogService = new ErrorLogService();
        //}

        #endregion

        #region Methods

        /// <summary>
        /// Logs the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        //public static int Log(Exception ex)
        //{
        //    try
        //    {
        //        if (ConfigurationManager.AppSettings["ErrorLog"] == "true")
        //            return Log(ex.Message, ex.StackTrace, ex.Source, ex.TargetSite != null ? ex.TargetSite.Name : string.Empty);
        //        else
        //            return 1;
        //    }
        //    catch (Exception a)
        //    {
        //        throw a;
        //    }
        //    return 0;
        //}

        /// <summary>
        /// Logs the specified error message.
        /// </summary>
        /// <param name="ErrorMessage">The error message.</param>
        /// <param name="ErrorStackTrace">The error stack trace.</param>
        /// <param name="ErrorSource">The error source.</param>
        /// <param name="ErrorTargetSite">The error target site.</param>
        /// <returns></returns>
        //public static int Log(string ErrorMessage, string ErrorStackTrace, string ErrorSource, string ErrorTargetSite)
        //{
        //    ErrorLog objErrorLog = new ErrorLog();

        //    objErrorLog.ErrorDate = DateTime.Now;
        //    //objErrorLog.loginId = ProjectSession.ClientConnectionString != null ? ProjectSession.LogInID : 0;

        //    objErrorLog.IPAddress = HttpContext.Current.Request.UserHostAddress;
        //    objErrorLog.ClientBrowser = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;

        //    objErrorLog.URL = HttpContext.Current.Request.Url.PathAndQuery;
        //    if ((HttpContext.Current.Request.UrlReferrer == null) == false)
        //    {
        //        objErrorLog.URLReferrer = HttpContext.Current.Request.UrlReferrer.PathAndQuery;
        //    }
        //    objErrorLog.ErrorMessage = ErrorMessage;
        //    objErrorLog.ErrorStackTrace = ErrorStackTrace;
        //    objErrorLog.ErrorSource = ErrorSource;
        //    objErrorLog.ErrorTargetSite = ErrorTargetSite;
        //    objErrorLog.QueryString = HttpContext.Current.Request.QueryString.ToString();
        //    objErrorLog.PostData = HttpContext.Current.Request.Form.ToString();

        //    System.Text.StringBuilder SessionInfo = new System.Text.StringBuilder();
        //    System.Collections.Specialized.NameObjectCollectionBase.KeysCollection objSessionKeys = HttpContext.Current.Session.Keys;
        //    int i = 0;
        //    while ((i <= (objSessionKeys.Count - 1)))
        //    {
        //        if ((HttpContext.Current.Session[i] == null) == false)
        //        {
        //            SessionInfo.Append((System.Environment.NewLine + ((i + 1).ToString() + (":->Name: " + objSessionKeys[i].ToString()))));
        //            if ((HttpContext.Current.Session[i].GetType().Name == "String") || (HttpContext.Current.Session[i].GetType().Name.Contains("Int")))
        //            {
        //                SessionInfo.Append((" <-->  Value:" + HttpContext.Current.Session[ConvertTo.String(objSessionKeys[i])]));
        //            }
        //            if ((HttpContext.Current.Session[i].GetType().Name == "DataTable"))
        //            {
        //                SessionInfo.Append((" <-->  Value: Number of rows:" + ((System.Data.DataTable)HttpContext.Current.Session[ConvertTo.String(objSessionKeys[i])]).Rows.Count.ToString()));
        //            }
        //        }
        //        i = (i + 1);
        //    }

        //    objErrorLog.SessionInfo = SessionInfo.ToString();
        //    _errorLogService = new ErrorLogService();
        //    return _errorLogService.AddErrorLog(objErrorLog);
        //}


        #endregion
    }
}