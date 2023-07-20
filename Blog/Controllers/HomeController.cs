using BeautyBook.Services.Contract;
using Blog.Infrastructure;
using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : BaseController
    {
        #region Fields
        private readonly AbstractBlogServices abstractBlogServices;
        #endregion

        #region Ctor
        public HomeController(AbstractBlogServices abstractBlogServices)
        {
            this.abstractBlogServices = abstractBlogServices;
        }
        #endregion

        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            ViewBag.SocialMediaSharePost = abstractBlogServices.BlogSharedCount();

            return View();
        }

        /// <summary>
        /// This methos use in get top 5 blogs that are most liked
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ManageMostLikedBlogData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractBlogServices.BlogTopArticles_All();

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// This methos use in get top 5 new blog
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ManageTopPostBlogData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractBlogServices.BlogTopPost_All();

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }
    }
}