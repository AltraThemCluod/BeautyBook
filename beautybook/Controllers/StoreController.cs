using BeautyBook.Common;
using BeautyBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class StoreController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StoreInside()
        {
            return View();
        }

        public ActionResult VendorDetails()
        {
            return View();
        }


        public ActionResult ViewProduct()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }

        public ActionResult CheckoutComplete(string OI = "MA==")
        {
            ViewBag.OrdId = Convert.ToInt32(ConvertTo.Base64Decode(OI));
            return View();
        }

        public ActionResult Invoice(string OrderId = "MA==")
        {
           ViewBag.OrdId = OrderId;
           return View();
        }

        public ActionResult InvoiceDetails()
        {
            return View();
        }

        public ActionResult Wishlist()
        {
            return View();
        }

    }
}