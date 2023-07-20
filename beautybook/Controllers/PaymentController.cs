using beautybook.Common;
using BeautyBook.Common;
using BeautyBook.Infrastructure;
using BeautyBook.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace beautybook.Controllers
{
    public class PaymentController : BaseController
    {
        // GET: Payment

        public ActionResult PaymentCallback(string OrdersId = "" , string respStatus = "")
        {

            if (respStatus == "A")
            {
                return RedirectToAction(Actions.CheckoutComplete, BeautyBook.Pages.Controllers.Store, new { OI = OrdersId });
            }
            else
            {
                return RedirectToAction(Actions.Error, BeautyBook.Pages.Controllers.Payment, new { Message = respStatus == null || respStatus == "" ? "Status not found" : respStatus });
            }
        }

        public ActionResult PackagesPaymentCallback(string PackagesId = "", string respStatus = "")
        {

            if (respStatus == "A")
            {
                return RedirectToAction(Actions.ManageEmailMarketing, BeautyBook.Pages.Controllers.Marketing, new { PackagesId = PackagesId });
            }
            else
            {
                return RedirectToAction(Actions.Error, BeautyBook.Pages.Controllers.Payment, new { Message = respStatus == null || respStatus == "" ? "Status not found" : respStatus });
            }
        }


        public ActionResult SMSPackagesPaymentCallback(string PackagesId = "", string respStatus = "")
        {

            if (respStatus == "A")
            {
                return RedirectToAction(Actions.ManageSMSMarketing, BeautyBook.Pages.Controllers.Marketing, new { PackagesId = PackagesId });
            }
            else
            {
                return RedirectToAction(Actions.Error, BeautyBook.Pages.Controllers.Payment, new { Message = respStatus == null || respStatus == "" ? "Status not found" : respStatus });
            }
        }

        public ActionResult Success(string OI = "")
        {
            ViewBag.OrdId = Convert.ToInt32(ConvertTo.Base64Decode(OI));
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}