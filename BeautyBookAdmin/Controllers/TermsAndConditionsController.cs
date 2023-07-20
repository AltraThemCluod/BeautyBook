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
    public class TermsAndConditionsController : BaseController
    {
        private readonly AbstractTermsAndConditionsServices abstractTermsAndConditionsServices = null;
      


        public TermsAndConditionsController 
            (
                AbstractTermsAndConditionsServices abstractTermsAndConditionsServices
                
            )
        {
            this.abstractTermsAndConditionsServices = abstractTermsAndConditionsServices;
         
        }

        public ActionResult Salon()
        {
            return View();
        }

        public ActionResult Vendor()
        {
            return View();
        }

      
        [HttpPost]
        public JsonResult TermsAndConditionsUpsert( long Id = 0, string TermsAndConditionsText = "")
        {
            TermsAndConditions model = new TermsAndConditions();
            model.Id = Id;
            model.TermsAndConditionsText = TermsAndConditionsText;
            model.CreatedBy = ProjectSession.AdminId;
            model.UpdatedBy = ProjectSession.AdminId;
            var result = abstractTermsAndConditionsServices.TermsAndConditions_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDataTermsAndConditionsById(string eptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractTermsAndConditions> successResult = abstractTermsAndConditionsServices.TermsAndConditions_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }
    }
}