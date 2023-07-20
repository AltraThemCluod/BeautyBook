using BeautyBook.Common;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using System;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class AccountController : Controller
    {
        #region Fields
        private readonly AbstractAdminServices abstractAdminServices;
        #endregion

        #region Ctor
        public AccountController(AbstractAdminServices abstractAdminServices)
        {
            this.abstractAdminServices = abstractAdminServices;
        }
        #endregion

        #region Methods
        
        public ActionResult LogIn()
        {
            return View();
        }
        //public ActionResult Logout()
        //{
        //    int UserType = 0;
        //    UserType = ProjectSession.UserTypeId;

        //    if (Request.Cookies["UserLogin"] != null)
        //    {

        //        abstractAdminServices.Admin_Logout(ProjectSession.UserId);

        //        string[] myCookies = Request.Cookies.AllKeys;
        //        foreach (string cookie in myCookies)
        //        {
        //            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
        //        }
        //    }
        //    Session.Clear();
        //    Session.Abandon();

        //    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Cache.SetNoStore();

        //    return RedirectToAction(Actions.LogIn, Pages.Controllers.Account);
        //}
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public JsonResult LogIn(string UserName, string Password)
        //{

        //    Admin model = new Admin();
        //    model.UserName = UserName;
        //    model.Password = Password;
        //    SuccessResult<AbstractAdmin> result = abstractAdminServices.Admin_Login(model);
        //    if (result != null && result.Code == 200 && result.Item != null)
        //    {   
        //        Session.Clear();
        //        ProjectSession.UserId = result.Item.Id;
        //        //ProjectSession.UserName = result.Item.FirstName;
        //        ProjectSession.UserName = result.Item.UserName;
        //        ProjectSession.ProfilePicture = result.Item.UrlStr;

        //        //ProjectSession.UserTypeId=result.Item.AdminTypeId;


        //        HttpCookie cookie = new HttpCookie("UserLogin");
        //        cookie.Values.Add("Id", result.Item.Id.ToString());
                
        //        cookie.Expires = DateTime.Now.AddDays(30);
        //        Response.Cookies.Add(cookie);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //[HttpPost]
        //public JsonResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        //{
        //    Admin Admin = new Admin();
        //    Admin.Id = ProjectSession.UserId;
        //    Admin.OldPassword = oldPassword;
        //    Admin.NewPassword = newPassword;
        //    Admin.ConfirmPassword = confirmPassword;
        //    SuccessResult<AbstractAdmin> result1 = abstractAdminServices.Admin_ChangePassword(Admin);
        //    return Json(result1, JsonRequestBehavior.AllowGet);

        //}
        
        public JsonResult UnAuthorizedAccess()
        {
            return Json(new { Code = 401,Message= "Unauthorized Access" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}