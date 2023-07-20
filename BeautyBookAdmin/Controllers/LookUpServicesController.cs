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
    public class LookUpServicesController : BaseController
    {
        private readonly AbstractLookUpServicesServices abstractLookUpServicesServices = null;
        
        public LookUpServicesController(AbstractLookUpServicesServices abstractLookUpServicesServices)
        
        {
            this.abstractLookUpServicesServices = abstractLookUpServicesServices;
        }

        //LookUpServices Index action
        public ActionResult ServicesCategory(string ParentId = "MjIy")
        {
            ViewBag.parentId = Convert.ToInt32(ConvertTo.Base64Decode(ParentId));
            return View();
        }
        //LookUpServices Index action
        public ActionResult SalonServices()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetDataParentLookUpServicesById(string eptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractLookUpServices> successResult = abstractLookUpServicesServices.LookUpServices_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ParentLookUpServicesUpsert(long Id = 0, string Name = "",decimal Price = 0 ,decimal Duration = 0)
        {
            LookUpServices model = new LookUpServices();
            model.Id = Id;
            model.Name = Name;
            model.Price = Price;
            model.Duration = Duration;
            model.SalonId = 0;
            model.UserId = 0;
            model.CreatedBy = ProjectSession.AdminId;
            model.UpdatedBy = ProjectSession.AdminId;
            var result = abstractLookUpServicesServices.LookUpServices_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //MasterProductBrand status actinact
        [HttpPost]
        public JsonResult ActiveInActiveLookUpParentServices(string ri = "MA==", long lookUpStatusId = 0)
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            long LookUpStatusId = lookUpStatusId;
            long LookUpStatusChangedBy = ProjectSession.AdminId;

            SuccessResult<AbstractLookUpServices> result = abstractLookUpServicesServices.LookUpServices_ActInact(Id, LookUpStatusId, LookUpStatusChangedBy);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Master Product Brand delete method
        [HttpPost]
        public JsonResult DeleteParentLookUpServices(string ptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ptId));
            long DeletedBy = ProjectSession.AdminId;
            abstractLookUpServicesServices.LookUpServices_Delete(Id, DeletedBy);
            return Json("Product Brand Deleted successfully", JsonRequestBehavior.AllowGet);
        }
        //getDate product Brand
        [HttpPost]
        public JsonResult GetDataLookUpServicesById(string eptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractLookUpServices> successResult = abstractLookUpServicesServices.LookUpServices_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }

        //Master Product Brand insert
        [HttpPost]
        public JsonResult LookUpServicesUpsert(long Id = 0, long ParentId = 0,string Name = "",decimal Duration = 0,decimal Price = 0)
        {
            {
                LookUpServices model = new LookUpServices();
                model.Id = Id;
                model.ParentId = ParentId;
                model.Name = Name;
                model.Price = Price;
                model.Duration = Duration;
                model.SalonId = 0;
                model.UserId = 0;
                model.CreatedBy = ProjectSession.AdminId;
                model.UpdatedBy = ProjectSession.AdminId;
                var result = abstractLookUpServicesServices.LookUpServices_Upsert(model);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
       
        //MasterProductBrand status actinact
        [HttpPost]
        public JsonResult ActiveInActiveLookUpServices(string ri = "MA==", long lookUpStatusId = 0)
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            long LookUpStatusId = lookUpStatusId;
            long LookUpStatusChangedBy = ProjectSession.AdminId;

            SuccessResult<AbstractLookUpServices> result = abstractLookUpServicesServices.LookUpServices_ActInact(Id, LookUpStatusId, LookUpStatusChangedBy);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Master Product Brand delete method
        [HttpPost]
        public JsonResult DeleteLookUpServices(string ptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ptId));
            long DeletedBy = ProjectSession.AdminId;
            abstractLookUpServicesServices.LookUpServices_Delete(Id, DeletedBy);
            return Json("Product Brand Deleted successfully", JsonRequestBehavior.AllowGet);
        }

        //LookUpServices all parent data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllSalonServicesData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel,long ParentId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                LookUpServices model = new LookUpServices();
                model.ParentId = ParentId;

                string search = Convert.ToString(requestModel.Search.Value);

                var response = abstractLookUpServicesServices.LookUpServices_ParentId(pageParam, search, model);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //LookUpServices all parent data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllSalonParentServicesData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long ParentId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);

                var response = abstractLookUpServicesServices.LookUpServices_All(pageParam, search, ParentId,0,0);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }
    }
}