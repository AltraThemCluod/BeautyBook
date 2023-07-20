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
    public class SMSPackagesController : BaseController
    {
        private readonly AbstractCreate_SMS_PackagesServices abstractCreate_SMS_PackagesServices = null;
        private readonly AbstractLookUpDurationServices abstractLookUpDurationServices = null;
        
        public SMSPackagesController(
                AbstractCreate_SMS_PackagesServices abstractCreate_SMS_PackagesServices,
                AbstractLookUpDurationServices abstractLookUpDurationServices
            )
        {
            this.abstractCreate_SMS_PackagesServices = abstractCreate_SMS_PackagesServices;
            this.abstractLookUpDurationServices = abstractLookUpDurationServices;
        }

        //SMSPackages Index action
        public ActionResult Index()
        {

            ViewBag.LookUpDuration = LookUpDurationDrp();

            return View();
        }

        //SMSPackages all parent data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllSMSPackages([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long LookUpDurationId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);

                var response = abstractCreate_SMS_PackagesServices.Create_SMS_Packages_All(pageParam, search, LookUpDurationId,0);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //ActiveInActiveSMS status actinact
        [HttpPost]
        public JsonResult ActiveInActiveSMS(string ri = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));

            SuccessResult<AbstractCreate_SMS_Packages> result = abstractCreate_SMS_PackagesServices.Create_SMS_Packages_ActInAct(Id);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Master Product Brand delete method
        [HttpPost]
        public JsonResult DeleteSMSPackages(string ptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ptId));
            long DeletedBy = ProjectSession.AdminId;
            abstractCreate_SMS_PackagesServices.Create_SMS_Packages_Delete(Id, DeletedBy);
            return Json("SMS Packages Deleted successfully", JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public JsonResult SMSPackagesUpsert(long Id = 0, string SMSPackagesName = "", decimal SMSPackagesPrice = 0, long LookUpDurationId = 0,long NoOfTextMessages = 0)
        {
            Create_SMS_Packages model = new Create_SMS_Packages();
            model.Id = Id;
            model.SMSPackagesName = SMSPackagesName;
            model.SMSPackagesPrice = SMSPackagesPrice;
            model.LookUpDurationId = LookUpDurationId;
            model.NoOfTextMessages = NoOfTextMessages;
            model.CreatedBy = ProjectSession.AdminId;
            model.UpdatedBy = ProjectSession.AdminId;
            var result = abstractCreate_SMS_PackagesServices.Create_SMS_Packages_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //getDate product Brand
        [HttpPost]
        public JsonResult GetDataSMSPackages(string eptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractCreate_SMS_Packages> successResult = abstractCreate_SMS_PackagesServices.Create_SMS_Packages_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }


    }
}