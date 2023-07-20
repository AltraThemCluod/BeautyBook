using BeautyBook.Common;
using BeautyBookAdmin.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using BeautyBook.Entities.Contract;

namespace BeautyBookAdmin.Controllers
{
    public class AuthenticationController : Controller
    {
        #region Fields
        private readonly AbstractAdminServices abstractAdminServices;
        #endregion
        #region Ctor
        public AuthenticationController(AbstractAdminServices abstractAdminServices)
        {
            this.abstractAdminServices = abstractAdminServices;
        }
        #endregion

        #region Methods
        [HttpGet]
        [ActionName(Actions.Signin)]
        public ActionResult Signin()
        {
            return View();
        }

        public ActionResult Signout()
        {
            var result = abstractAdminServices.Admin_Logout(ProjectSession.AdminId);
            if (result == true)
            {
                if (Request.Cookies["AdminLogin"] != null)
                {
                    string[] myCookies = Request.Cookies.AllKeys;
                    foreach (string cookie in myCookies)
                    {
                        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                    }
                }
            }
           

            Session.Clear();
            Session.Abandon();

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            return RedirectToAction(Actions.Signin, Pages.Controllers.Authentication);
        }
        #endregion

        #region Json Result Post Method 
        [HttpPost]
        public JsonResult Signin(string username, string password)
        {
            SuccessResult<AbstractAdmin> result = abstractAdminServices.Admin_Login(username, password);
            if (result.Code == 200 && result.Item != null)
            {
                Session.Clear();
                ProjectSession.AdminId = result.Item.Id;
                ProjectSession.AdminName = result.Item.FirstName + ' ' + result.Item.LastName;
                HttpCookie cookie = new HttpCookie("AdminLogin");
                cookie.Values.Add("Id", result.Item.Id.ToString());

                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}