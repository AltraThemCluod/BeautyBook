using BeautyBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class SalonsController : BaseController
    {
        public ActionResult EditSalonInfo()
        {
            return View();
        }
        public ActionResult SalonsDetails()
        {
            return View();
        }
    }
}