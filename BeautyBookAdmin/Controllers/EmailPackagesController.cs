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
    public class EmailPackagesController : BaseController
    {
        private readonly AbstractCreate_EmailMsg_PackagesServices abstractCreate_EmailMsg_PackagesServices = null;
        private readonly AbstractLookUpDurationServices abstractLookUpDurationServices = null;
        
        public EmailPackagesController(
                AbstractCreate_EmailMsg_PackagesServices abstractCreate_EmailMsg_PackagesServices,
                AbstractLookUpDurationServices abstractLookUpDurationServices
            )
        {
            this.abstractCreate_EmailMsg_PackagesServices = abstractCreate_EmailMsg_PackagesServices;
            this.abstractLookUpDurationServices = abstractLookUpDurationServices;
        }

        //EmailPackages Index action
        public ActionResult Index()
        {

            ViewBag.LookUpDuration = LookUpDurationDrp();

            return View();
        }

        //EmailPackages all parent data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllEmailPackages([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long LookUpDurationId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);

                var response = abstractCreate_EmailMsg_PackagesServices.Create_EmailMsg_Packages_All(pageParam, search, LookUpDurationId,0);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //ActiveInActiveEmail status actinact
        [HttpPost]
        public JsonResult ActiveInActiveEmail(string ri = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));

            SuccessResult<AbstractCreate_EmailMsg_Packages> result = abstractCreate_EmailMsg_PackagesServices.Create_EmailMsg_Packages_ActInAct(Id);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Email Packages delete method
        [HttpPost]
        public JsonResult DeleteEmailPackages(string ptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ptId));
            long DeletedBy = ProjectSession.AdminId;
            abstractCreate_EmailMsg_PackagesServices.Create_EmailMsg_Packages_Delete(Id, DeletedBy);
            return Json("Email Packages Deleted successfully", JsonRequestBehavior.AllowGet);
        }

        //LookUpDuration drp data get
        public IList<SelectListItem> LookUpDurationDrp()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;

                var models = abstractLookUpDurationServices.LookUpDuration_All(pageParam, "");

                foreach (var master in models.Values)
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

        //SMS Packages add
        //[HttpPost]
        public JsonResult EmailPackagesUpsert(long Id = 0, string EmailMsgPackagesName = "", decimal EmailMsgPackagesPrice = 0, long LookUpDurationId = 0, long NoOfMessages = 0)
        {
            Create_EmailMsg_Packages model = new Create_EmailMsg_Packages();
            model.Id = Id;
            model.EmailMsgPackagesName = EmailMsgPackagesName;
            model.EmailMsgPackagesPrice = EmailMsgPackagesPrice;
            model.LookUpDurationId = LookUpDurationId;
            model.NoOfMessages = NoOfMessages;
            model.CreatedBy = ProjectSession.AdminId;
            model.UpdatedBy = ProjectSession.AdminId;
            var result = abstractCreate_EmailMsg_PackagesServices.Create_EmailMsg_Packages_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //getDate product Brand
        [HttpPost]
        public JsonResult GetDataEmailPackages(string eptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractCreate_EmailMsg_Packages> successResult = abstractCreate_EmailMsg_PackagesServices.Create_EmailMsg_Packages_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }


    }
}