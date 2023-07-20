using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook_Vendor.Controllers
{
    public class ApplicationFormController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApprovelProcess()
        {
            return View();
        }
    }
}