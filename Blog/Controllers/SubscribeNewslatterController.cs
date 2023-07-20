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
    public class SubscribeNewslatterController : BaseController
    {

        #region Fields
        private readonly AbstractBlogSubscribeNewslatterServices abstractBlogSubscribeNewslatterServices;
        #endregion

        #region Ctor
        public SubscribeNewslatterController(AbstractBlogSubscribeNewslatterServices abstractBlogSubscribeNewslatterServices)
        {
            this.abstractBlogSubscribeNewslatterServices = abstractBlogSubscribeNewslatterServices;
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method use to listing of user's subscribe newslatter email
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ManageSubscribeNewslatterData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractBlogSubscribeNewslatterServices.BlogSubscribeNewslatter_All(pageParam, search);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }
    }
}