using BeautyBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class PackagesController : BaseController
    {
        public ActionResult PackagesDetails()
        {
            return View();
        }
        public ActionResult ManagePackages()
        {
            return View();
        }
    }
}