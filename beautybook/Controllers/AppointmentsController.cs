using beautybook.Models;
using BeautyBook.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBook.Controllers
{
    public class AppointmentsController : BaseController
    {
        public ActionResult AppointmentDetails()
        {
            return View();
        }
        public ActionResult ManageAppointments()
        {
            return View();
        }
        public ActionResult Invoice()
        {
            return View();
        }

        public ActionResult PublicInvoice()
        {
            return View();
        }

        public void GetQrCode()
        {
            string SellerName = "Amipara kartik R.";
            string TaxNo = "311215143500003";
            DateTime DtVal = DateTime.UtcNow;
            Double TotalAmt = 10000;
            Double TaxAmt = 1000;

            string GenFile = GenerateQrCode(SellerName, TaxNo, DtVal, TotalAmt, TaxAmt);

        }

        public string GenerateQrCode(string SellerName, string TaxNo, DateTime DtTime, Double Total, Double Tax)
        {
            TLVCls tlv = new TLVCls(SellerName, TaxNo, DtTime, Total, Tax);
            System.Drawing.Image img = tlv.toQrCode();

            string fileName = "";

            if (img != null)
            {
                string basePath = "SimplifiedInvoice/";
                fileName = DateTime.UtcNow.ToString("yyyyMMddhhmmssfff") + "_" + "QRCode";
                string path = Server.MapPath("~/" + basePath);
                if (!Directory.Exists(Path.Combine(HttpContext.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Server.MapPath("~/" + basePath));
                }
                img.Save(HttpContext.Server.MapPath("~/" + basePath + fileName + ".png"));
            }

            return fileName;
        }

    }
}