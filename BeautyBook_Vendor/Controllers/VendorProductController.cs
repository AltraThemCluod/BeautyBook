using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook_Vendor.Controllers
{
    public class VendorProductController : Controller
    {
        public ActionResult ManageProduct()
        {
            return View();
        }

        public ActionResult ProductDetails()
        {
            return View();
        }
    }
}