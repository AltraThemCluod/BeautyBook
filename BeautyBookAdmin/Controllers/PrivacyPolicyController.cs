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
    public class PrivacyPolicyController : BaseController
    {
        private readonly AbstractPrivacyPolicyServices abstractPrivacyPolicyServices = null;
      


        public PrivacyPolicyController
            (
                AbstractPrivacyPolicyServices abstractPrivacyPolicyServices

            )
        {
            this.abstractPrivacyPolicyServices = abstractPrivacyPolicyServices;
         
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
        public JsonResult PrivacyPolicyUpsert( long Id = 0, string PrivacyPolicyText = "", long Type = 0)
        {
            PrivacyPolicy model = new PrivacyPolicy();
            model.Id = Id;
            model.PrivacyPolicyText = PrivacyPolicyText;
            model.Type = Type;
            var result = abstractPrivacyPolicyServices.PrivacyPolicy_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDataPrivacyPolicyById(string eptId = "MA==" , long Type = 0)
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractPrivacyPolicy> successResult = abstractPrivacyPolicyServices.PrivacyPolicy_ById(Id , Type);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }
    }
}