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
using System.Web;
using System.Web.Mvc;

namespace BeautyBookAdmin.Controllers
{
    public class SalonOwnerController : BaseController
    {
        private readonly AbstractUsersServices abstractUsersServices = null;
        private readonly AbstractSalonsServices abstractSalonsServices = null;

        public SalonOwnerController(AbstractUsersServices abstractUsersServices , AbstractSalonsServices abstractSalonsServices)
        {
            this.abstractUsersServices = abstractUsersServices;
            this.abstractSalonsServices = abstractSalonsServices;
        }

        //SalonOwner Index action
        public ActionResult Index()
        {
            return View();
        }

        //SalonOwner Salons action
        public ActionResult Salons(string SalonOwnerId = "MjIy")
        {
            ViewBag.salonOwnerId = Convert.ToInt32(ConvertTo.Base64Decode(SalonOwnerId));
            return View();
        }


        //Salon owner all data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel , long LookUpStatusId = 0 , string Gender = "")
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);

                    Users model = new Users();
                    model.LookUpUserTypeId = 2;
                    model.LookUpStatusId = LookUpStatusId;
                    model.Gender = Gender;
                    var response = abstractUsersServices.Users_All(pageParam, search , model);

                    totalRecord = (int)response.TotalRecords;
                    filteredRecord = (int)response.TotalRecords;

                    return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //Salon owner all salons list
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataSalons([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel , long UserId = 0 , long LookUpStatusId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractSalonsServices.Salons_ByUserId(pageParam, search, UserId  , LookUpStatusId);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //SalonOwner status actinact
        [HttpPost]
        public JsonResult ActiveInActive(string ri = "MA==" , long lookUpStatusId = 0)
        {
            int Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            SuccessResult<AbstractUsers> result = abstractUsersServices.Users_ActInact(Id , lookUpStatusId, 1);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Salon change status 
        [HttpPost]
        public JsonResult ChangeStatusSalon(string SalonId = "MA==", long changeSalonstatus = 0)
        {
            long salonId = Convert.ToInt32(ConvertTo.Base64Decode(SalonId));
            SuccessResult<AbstractSalons> result = abstractSalonsServices.Salons_ApprovedUnApproved(salonId, changeSalonstatus, 1);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}