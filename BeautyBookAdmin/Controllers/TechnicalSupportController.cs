using DataTables.Mvc;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using BeautyBookAdmin.Infrastructure;
using BeautyBookAdmin.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BeautyBookAdmin.Controllers
{
    public class TechnicalSupportController : BaseController
    {
        private readonly AbstractTechnicalSupportServices abstractTechnicalSupportServices = null;


        public TechnicalSupportController(
                AbstractTechnicalSupportServices abstractTechnicalSupportServices
            )
        {
            this.abstractTechnicalSupportServices = abstractTechnicalSupportServices;
        }

        //Index action
        public ActionResult Index()
        {
            return View();
        }

        //TechnicalSupport all data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllTechnicalSupport([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel,long UserType= 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractTechnicalSupportServices.TechnicalSupport_All(pageParam, search,0,0, UserType);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }
    }
}