using BeautyBook.Common;
using BeautyBook.Infrastructure;
using BeautyBook.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class PosController : BaseController
    {
        public ActionResult Index(string ri = "MA==")
        {


            if (Request.Cookies["SalonName"] != null)
            {
                ViewBag.SalonName = Request.Cookies["SalonName"].Value;
            }

            if (Request.Cookies["PosAuthEmail"] != null)
            {
                ViewBag.PosAuthEmail = Request.Cookies["PosAuthEmail"].Value;
            }


            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            ViewBag.ri = DecodeId;
            
            return View();
        }

        public ActionResult Sessions()
        {
            return View();
        }
        
        public ActionResult ReturnInvoice()
        {
            return View();
        }

        public ActionResult CreateSessions()
        {
            return View();
        }

		public ActionResult ManageSession(string ri = "MA==")
		{

			ViewBag.SessionId = ri;
			return View();
		}

		public ActionResult Orders(string ri = "MA==")
        {
			int DecodeSessionId = Convert.ToInt32(ConvertTo.Base64Decode(ri));
			ViewBag.SessionId = DecodeSessionId;
			return View();
        }

        public ActionResult PosAuthentication()
        {
            return View();
        }   
        
        public ActionResult SiplifedInvoice()
        {
            return View();
        }

        public ActionResult PosInvoice(string ri = "MA==")
        {

            ViewBag.ri = ri;

            return View();
        }
    }
}