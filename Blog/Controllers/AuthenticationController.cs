using BeautyBook.Common;
using BeautyBook.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBook.Entities.Contract;
using static Blog.Infrastructure.Enums;

namespace Blog.Controllers
{
    public class AuthenticationController : Controller
    {
        #region Fields
        private readonly AbstractBlogUsersServices abstractBlogUsersServices;
        #endregion

        #region Ctor
        public AuthenticationController(AbstractBlogUsersServices abstractBlogUsersServices)
        {
            this.abstractBlogUsersServices = abstractBlogUsersServices;
        }
        #endregion

        public ActionResult AuthLogin()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AuthUserSignIn(string UserName,string Password)
        {

            AuthLogin authObj = new AuthLogin();
            authObj.UserName = UserName;
            authObj.Password = Password;    

            SuccessResult<AbstractBlogUsers> result = abstractBlogUsersServices.BlogUser_Login(authObj);

            if (result.Code == 200 && result.Item != null)
            {
                Session.Clear();
                ProjectSession.AdminId = result.Item.Id;
                ProjectSession.AdminName = result.Item.FirstName + ' ' + result.Item.LastName;
                HttpCookie cookie = new HttpCookie("BlogUserLogin");
                cookie.Values.Add("Id", result.Item.Id.ToString());

                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// This methos use to users account logout
        /// </summary>
        /// <returns></returns>
        public ActionResult Signout()
        {
            if (Request.Cookies["BlogUserLogin"] != null)
            {
                string[] myCookies = Request.Cookies.AllKeys;
                foreach (string cookie in myCookies)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }
            }

            Session.Clear();
            Session.Abandon();

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            return RedirectToAction("AuthLogin", "Authentication");
        }
    }
}