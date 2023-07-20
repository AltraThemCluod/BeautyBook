using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using BeautyBook.Common;
using BeautyBook.Pages;

namespace BeautyBook.Infrastructure
{
    public class BaseController : Controller
    {
        #region Fields
        public static string AdminUserName;
        public static string CheckType;
        public static string CheckLastDate;
        #endregion

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (ProjectSession.EmployeePermission == null)
                {
                    if (ProjectSession.UserTypeId == 2)
                    {
                        filterContext.Result = new RedirectResult("~/Authentication/Signin");
                    }
                    else if (ProjectSession.UserTypeId == 3)
                    {
                        filterContext.Result = new RedirectResult("~/Authentication/EmployeeSignIn");
                    }
                }

                bool IsAllow = false;
                HttpCookie reqCookie = Request.Cookies["UserTypeId"];
                HttpCookie reqCookieSalon = Request.Cookies["SalonId"];
                HttpCookie tokenAndType = Request.Cookies["LoginDetails"];

                var Authtoken = "";
                if (tokenAndType != null)
                {
                    Authtoken = Convert.ToString(tokenAndType["IsAuthtokenValid"]);
                }

                if (Authtoken != null && Authtoken != "" && ProjectSession.EmployeePermission != null)
                {
                    int UserTypeId = ConvertTo.Integer(reqCookie.Value.ToString());
                    int SalonId = 0;

                    string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToString();
                    string ActionName = filterContext.ActionDescriptor.ActionName.ToString();

                    if (reqCookieSalon != null)
                    {
                        SalonId = ConvertTo.Integer(ConvertTo.Base64Decode(reqCookieSalon.Value.ToString()));
                    }
                    if (SalonId > 0 || ControllerName.ToString().Contains("Salons"))
                    {
                        if (UserTypeId == 1 && Pages.Controllers.AdminAccess.Contains(ControllerName))
                        {
                            IsAllow = true;
                        }
                        else if (UserTypeId == 2 && Pages.Controllers.SalonOwnerAccess.Contains(ControllerName))
                        {
                            IsAllow = true;
                        }
                        else if (
                            UserTypeId == 3 && 
                            ProjectSession.EmployeePermissionController.Item.ConvertAll(x => x.Controller.ToString()).Distinct().ToArray().Contains(ControllerName) &&
                            ProjectSession.EmployeePermissionController.Item.ConvertAll(x => x.Action.ToString()).Distinct().ToArray().Contains(ActionName))
                        {
                            IsAllow = true;
                        }
                        else if (UserTypeId == 4 && Pages.Controllers.CustomerAccess.Contains(ControllerName))
                        {
                            IsAllow = true;
                        }
                        else if (UserTypeId == 5 && Pages.Controllers.SellerAccess.Contains(ControllerName))
                        {
                            IsAllow = true;
                        }
                    }
                }
                //if (IsAllow == true)
                //{
                //    base.OnActionExecuting(filterContext);
                //}
                //else
                //{
                //    throw new Exception("");
                //}
                if (!IsAllow)
                {
                    if (ProjectSession.UserTypeId == 2)
                    {
                        filterContext.Result = new RedirectResult("~/Authentication/Signin");
                    }
                    else if(ProjectSession.UserTypeId == 3)
                    {
                        filterContext.Result = new RedirectResult("~/Authentication/EmployeeSignIn");
                    }

                    return;
                }
                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                if (ProjectSession.UserTypeId == 2)
                {
                    filterContext.Result = new RedirectResult("~/Authentication/Signin");
                }
                else if (ProjectSession.UserTypeId == 3)
                {
                    filterContext.Result = new RedirectResult("~/Authentication/EmployeeSignIn");
                }
            }

        }

    }
}