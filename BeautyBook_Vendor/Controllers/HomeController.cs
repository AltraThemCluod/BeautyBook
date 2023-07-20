using BeautyBook.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook_Vendor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string FromDate = "", string ToDate = "")
        {
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            return View();
        }
        public ActionResult Change(String LanguageAbbrevation,string con, string act)
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