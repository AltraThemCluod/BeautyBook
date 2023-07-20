using BeautyBook.Infrastructure;
using BeautyBook.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class AppointmentInvoiceController : BaseController
    {
        public ActionResult Index(string InvoiceId = "MA==")
        {
            ViewBag.InvoiceId = InvoiceId;
            return View();
        }

        public ActionResult InvoiceChange(String LanguageAbbrevation, string InvoiceIdStr = "")
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("Languages");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);

            return RedirectToAction(Actions.Index, BeautyBook.Pages.Controllers.AppointmentInvoice, new { Area = "", InvoiceId = Convert.ToString(InvoiceIdStr) });
        }

    }
}