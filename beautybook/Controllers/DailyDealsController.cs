using BeautyBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class DailyDealsController : BaseController
    {
        public ActionResult ManageDailyDeals()
        {
            return View();
        }
        public ActionResult DailyDealsDetails()
        {
            return View();
        }
    }
}