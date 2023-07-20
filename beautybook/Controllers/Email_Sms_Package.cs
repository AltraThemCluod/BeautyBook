using BeautyBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class Email_Sms_PackageController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}