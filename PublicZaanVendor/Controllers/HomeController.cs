using BeautyBook.Common;
using PublicZaanVendor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PublicZaanVendor.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("home-store")]
        public ActionResult HomeStore()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendScheduleDetails(string FullName = "",string PhoneNumber = "",string DuringDay = "")
        {
            EmailHelper.SendScheduleEmail(FullName, PhoneNumber, DuringDay);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendConsultationDetails(string Name = "", string Email = "", string Subject = "")
        {
            EmailHelper.SendConsultationDetails(Name, Email, Subject);
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        ////////////////////////////////////////////////////////////////
        public ActionResult Change(String LanguageAbbrevation, string con, string act)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("Languages");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);

            return RedirectToAction(act, con);
        }

    }
}