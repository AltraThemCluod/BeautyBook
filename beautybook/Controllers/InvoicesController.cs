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
    public class InvoicesController : BaseController
    {
        public ActionResult SiplifedInvoice()
        {
            return View();
        }

        public ActionResult ReturnInvoice(string RI = "MA==")
        {
            ViewBag.ReturnInvoiceId = RI;
            return View();
        }

        public ActionResult TaxInvoice()
        {
            return View();
        }

        public ActionResult TaxInvoiceDetails()
        {
            return View();
        }
    }
}