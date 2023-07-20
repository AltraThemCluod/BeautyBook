using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using Blog.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Blog.Infrastructure.Enums;

namespace Blog.Controllers
{
    public class ProfileController : BaseController
    {
        #region Fields
        private readonly AbstractBlogUsersServices abstractBlogUsersServices;
        #endregion

        #region Ctor
        public ProfileController(AbstractBlogUsersServices abstractBlogUsersServices)
        {
            this.abstractBlogUsersServices = abstractBlogUsersServices;
        }
        #endregion

        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            ViewBag.MasterGender = MasterGender();

            var model = abstractBlogUsersServices.BlogUsers_ById((int)ProjectSession.AdminId);
            return View(model.Item);
        }

        public ActionResult ChangePassword()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            return View();
        }

        /// <summary>
        /// this methos use to update admin and employee profile
        /// </summary>
        /// <param name="blogUsers"></param>
        /// <param name="ProfileUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserProfile(BlogUsers blogUsers , HttpPostedFileBase ProfileUrl)
        {

            if(ProfileUrl != null)
            {
                string UserTyp;
                if (ProjectSession.UserTypeId== 1) UserTyp = "Admin";
                else UserTyp = "Employee";

                string basePath = "UserProfile/" + UserTyp + "/";
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + Path.GetFileName(ProfileUrl.FileName);
                string path = Server.MapPath("~/" + basePath);
                if (!Directory.Exists(Path.Combine(HttpContext.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Server.MapPath("~/" + basePath));
                }
                ProfileUrl.SaveAs(HttpContext.Server.MapPath("~/" + basePath + fileName));
                blogUsers.ProfileUrl = basePath + fileName;
            }

            blogUsers.UserTypeId = ProjectSession.UserTypeId;

            SuccessResult<AbstractBlogUsers> result = abstractBlogUsersServices.BlogUsers_Upsert(blogUsers);
            if (result.Item == null)
            {
                TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.error.ToString());
                return RedirectToAction("Index", "Profile", new { Area = ""});
            }
            TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.success.ToString());
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        
        /// <summary>
        /// this function use to store gender data in listing
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> MasterGender()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;

                var GenderString = "[{\"Id\":1,\"Name\":\"Male\"},{\"Id\":2,\"Name\":\"FeMale\"}]";

                List<GenderRoot> genderd = JsonConvert.DeserializeObject<List<GenderRoot>>(GenderString.ToString());

                foreach (var master in genderd)
                {
                    items.Add(new SelectListItem() { Text = master.Name.ToString(), Value = Convert.ToString(master.Id) });
                }
                return items;
            }
            catch (Exception)
            {
                return items;
            }
        }


        /// <summary>
        /// This method use to change users password
        /// </summary>
        /// <param name="blogUsersChangePassword"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(BlogUsersChangePassword blogUsersChangePassword)
        {
            blogUsersChangePassword.Id = (int)ProjectSession.AdminId;
            SuccessResult<AbstractBlogUsers> result = abstractBlogUsersServices.BlogUsers_ChangePassword(blogUsersChangePassword);
            if (result.Item == null)
            {
                TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.error.ToString());
                return RedirectToAction("ChangePassword", "Profile", new { Area = "" });
            }
            //TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.success.ToString());
            return RedirectToAction("Signout", "Authentication", new { Area = "" });
        }
    }
}