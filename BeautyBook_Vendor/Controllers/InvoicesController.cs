using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class InvoicesController : Controller
    {
        public ActionResult TaxInvoice()
        {
            return View();
        }

        public ActionResult ReturnInvoice(string RI = "MA==")
        {
            ViewBag.ReturnInvoiceId = RI;
            return View();
        }
    }
}