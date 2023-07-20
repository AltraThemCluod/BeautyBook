using BeautyBook.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.V1;

namespace Blog.Infrastructure
{

    public class BaseController : Controller
    {
        public BaseController()
        {

        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {

                bool isAllow = false;
                HttpCookie reqCookie = Request.Cookies["BlogUserLogin"];
                if (reqCookie != null)
                {
                    ProjectSession.AdminId = Convert.ToInt32(Convert.ToString(reqCookie["Id"]));
                }
                else
                {
                    ProjectSession.AdminId = 0;
                }

                string var_sql = "select * from BlogUsers where Id = " + ProjectSession.AdminId;

                SqlConnection con = new SqlConnection(Configurations.ConnectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(var_sql, con);
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    ProjectSession.AdminName = dt.Rows[0]["FirstName"].ToString() + ' ' + dt.Rows[0]["LastName"].ToString();
                    ProjectSession.AdminProfile = dt.Rows[0]["ProfileUrl"].ToString();
                    ProjectSession.UserTypeId = (int)dt.Rows[0]["UserTypeId"];

                    string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();


                    string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToString();

                    if (Convert.ToInt32(dt.Rows[0]["UserTypeId"]) == 1 && UserAccessData.AdminAccess.Contains(ControllerName))
                    {
                        isAllow = true;
                    }
                    else if (Convert.ToInt32(dt.Rows[0]["UserTypeId"]) == 2 && UserAccessData.EmployeeAccess.Contains(ControllerName))
                    {
                        isAllow = true;
                    }
                }
                else
                {
                    isAllow = false;
                }

                if (!isAllow)
                {
                    filterContext.Result = new RedirectResult("~/Authentication/AuthLogin");
                    return;
                }
                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Authentication/AuthLogin");
            }
        }

    }
}