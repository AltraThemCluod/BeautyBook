using BeautyBook.Common;
using BeautyBookAdmin.Pages;
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

namespace BeautyBookAdmin.Infrastructure
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
                HttpCookie reqCookie = Request.Cookies["AdminLogin"];
                if (reqCookie != null)
                {
                    ProjectSession.AdminId = Convert.ToInt32(Convert.ToString(reqCookie["Id"]));
                }
                else
                {
                    ProjectSession.AdminId = 0;
                }

                string var_sql = "select * from Admin where Id = " + ProjectSession.AdminId;

                SqlConnection con = new SqlConnection(Configurations.ConnectionString);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(var_sql, con);
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    ProjectSession.AdminName = dt.Rows[0]["FirstName"].ToString() + ' ' + dt.Rows[0]["LastName"].ToString();
                    isAllow = true;
                }
                else
                {
                    isAllow = false;
                }

                if (!isAllow)
                {
                    filterContext.Result = new RedirectResult("~/Authentication/Signin");
                    return;
                }
                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Authentication/Signin");
            }
        }

    }
}