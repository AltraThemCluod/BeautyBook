using BeautyBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class MyProfileController : BaseController
    {
        public ActionResult EditProfile()
        {
            return View();
        }
    }
}