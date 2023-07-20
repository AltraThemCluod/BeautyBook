using BeautyBook.Common;
using BeautyBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class SalonServicesController : BaseController
    {
        public ActionResult ManageSalonServices()
        {
            return View();
        }

        public ActionResult SalonServiceDetails(string SalonServices = "")
        {
            ViewBag.SalonId = ConvertTo.Integer(ConvertTo.Base64Decode(SalonServices));
            return View();
        }
    }
}