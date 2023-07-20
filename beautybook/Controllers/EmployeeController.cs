using BeautyBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class EmployeeController : BaseController
    {
        public ActionResult EmployeeDetails()
        {
            return View();
        }
        public ActionResult ManageEmployee()
        {
            return View();
        }
        public ActionResult EmployeeLeaveManagement()
        {
            return View();
        }
        public ActionResult EmployeeWorksheet()
        {
            return View();
        }
        public ActionResult EmployeePermission()
        {
            return View();
        }
    }
}