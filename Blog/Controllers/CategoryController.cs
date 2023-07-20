using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using Blog.Infrastructure;
using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Blog.Infrastructure.Enums;

namespace Blog.Controllers
{
    public class CategoryController : BaseController
    {

        #region Fields
        private readonly AbstractBlogCategoryServices abstractBlogCategoryServices;
        #endregion

        #region Ctor
        public CategoryController(AbstractBlogCategoryServices abstractBlogCategoryServices)
        {
            this.abstractBlogCategoryServices = abstractBlogCategoryServices;
        }
        #endregion

        public ActionResult Index(string ri = "MA==")
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(ri));

            ViewBag.DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(ri));

            var model = abstractBlogCategoryServices.BlogCategory_ById(DecodeId);

            return View(model.Item);
        }

        /// <summary>
        /// This method use to listing category data
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ManageCategoryData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractBlogCategoryServices.BlogCategory_All(pageParam, search);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// This method use to active and in-active category data
        /// </summary>
        /// <param name="Id">Category Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ActiveInActiveCategory(string Id = "MA==")
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(Id));
            SuccessResult<AbstractBlogCategory> result = abstractBlogCategoryServices.BlogCategory_ActInAct(DecodeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method use to delete category data
        /// </summary>
        /// <param name="Id">Category Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteCategory(string Id = "MA==")
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(Id));
            SuccessResult<AbstractBlogCategory> result = abstractBlogCategoryServices.BlogCategory_Delete(DecodeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method use to blog category data add and update
        /// </summary>
        /// <param name="blogCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBlogCategory(BlogCategory blogCategory)
        {

            SuccessResult<AbstractBlogCategory> result = abstractBlogCategoryServices.BlogCategory_Upsert(blogCategory);
            if (result.Item == null)
            {
                TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.error.ToString());
                return RedirectToAction("Index", "Category", new { Area = "" });
            }
            TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.success.ToString());
            return RedirectToAction("Index", "Category", new { Area = "" });
        }
    }
}