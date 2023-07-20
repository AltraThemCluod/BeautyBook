using BeautyBook.Common;
using BeautyBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class MarketingController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageEmailMarketing(string PackagesId = "MA==")
        {
            ViewBag.PackagesId = Convert.ToInt32(ConvertTo.Base64Decode(PackagesId));
            return View();
        }

        public ActionResult EmailMarketingDetails()
        {
            return View();
        }
        public ActionResult ManageSmsMarketing(string PackagesId = "MA==")
        {
            ViewBag.PackagesId = Convert.ToInt32(ConvertTo.Base64Decode(PackagesId));

            return View();
        }

        public ActionResult SmsMarketingDetails()
        {
            return View();
        }
        public ActionResult EmailTemplateEditor()
        {
            return View();
        }
        public ActionResult ChooseEmailTemplate()
        {
            return View();
        }

    }
}