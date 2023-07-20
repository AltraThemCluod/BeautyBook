using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using Blog.Infrastructure;
using DataTables.Mvc;
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
    public class BlogUsersController : BaseController
    {

        #region Fields
        private readonly AbstractBlogUsersServices abstractBlogUsersServices;
        #endregion

        #region Ctor
        public BlogUsersController(AbstractBlogUsersServices abstractBlogUsersServices)
        {
            this.abstractBlogUsersServices = abstractBlogUsersServices;
        }
        #endregion

        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            return View();
        }

        public ActionResult Details(string ri = "MA==")
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            ViewBag.MasterGender = MasterGender();
            ViewBag.MasterUserType = MasterUserType();


            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(ri));

            ViewBag.DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(ri));

            var model = abstractBlogUsersServices.BlogUsers_ById(DecodeId);

            return View(model.Item);
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
        /// this function use to store user type data in listing
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> MasterUserType()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;

                var UserTypeString = "[{\"Id\":1,\"UserType\":\"Admin\"},{\"Id\":2,\"UserType\":\"Employee\"}]";

                List<BlogUserType> userType = JsonConvert.DeserializeObject<List<BlogUserType>>(UserTypeString.ToString());

                foreach (var master in userType)
                {
                    items.Add(new SelectListItem() { Text = master.UserType.ToString(), Value = Convert.ToString(master.Id) });
                }
                return items;
            }
            catch (Exception)
            {
                return items;
            }
        }


        /// <summary>
        /// This method use to listing users data
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ManageUsersData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractBlogUsersServices.BlogUsers_All(pageParam, search);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// This method use to active and in-active users data
        /// </summary>
        /// <param name="Id">Users Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ActiveInActiveUsers(string Id = "MA==")
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(Id));
            SuccessResult<AbstractBlogUsers> result = abstractBlogUsersServices.BlogUsers_ActInAct(DecodeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method use to delete users data
        /// </summary>
        /// <param name="Id">Users Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteUsers(string Id = "MA==")
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(Id));
            SuccessResult<AbstractBlogUsers> result = abstractBlogUsersServices.BlogUsers_Delete(DecodeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// this methos use to create and update admin and employee profile
        /// </summary>
        /// <param name="blogUsers"></param>
        /// <param name="ProfileUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUserProfile(BlogUsers blogUsers, HttpPostedFileBase ProfileUrl)
        {

            if (ProfileUrl != null)
            {
                string UserTyp;
                if (ProjectSession.UserTypeId == 1) UserTyp = "Admin";
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

            SuccessResult<AbstractBlogUsers> result = abstractBlogUsersServices.BlogUsers_Upsert(blogUsers);
            if (result.Item == null)
            {
                TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.error.ToString());
                return RedirectToAction("Index", "BlogUsers", new { Area = "" });
            }
            TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.success.ToString());
            return RedirectToAction("Index", "BlogUsers", new { Area = "" });
        }
    }
}