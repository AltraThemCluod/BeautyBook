using BeautyBook.APICommon;
using BeautyBookApi.Models;
using BeautyBook.Common;
using BeautyBookApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;
using BeautyBook.Entities.V1;
using System.IO;
using System.Security.Claims;
using System.Web.Http.Cors;
using beautybook.Models;

namespace BeautyBookApi.Controllers.V1
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class POSPaymentV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractPOSPaymentServices abstractPOSPaymentServices;
        private readonly AbstractSalonsServices abstractSalonsServices;
        #endregion

        #region Cnstr
        public POSPaymentV1Controller(AbstractPOSPaymentServices abstractPOSPaymentServices, AbstractSalonsServices abstractSalonsServices)
        {
            this.abstractPOSPaymentServices = abstractPOSPaymentServices;
            this.abstractSalonsServices = abstractSalonsServices;
        }
        #endregion

        //POSPayment_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSPayment_Upsert")]
        public async Task<IHttpActionResult> POSPayment_Upsert(POSPayment pOSPayment)
        {
            if(pOSPayment.SalonId > 0)
            {
                var response = abstractSalonsServices.Salons_ById(pOSPayment.SalonId);

                if(response.Item != null)
                {   
                   var getQrCodeURL = GetQrCode(
                       response.Item.SalonName,
                       response.Item.TaxNumber,
                       ConvertTo.Double(pOSPayment.TotalAmount),
                       ConvertTo.Double(pOSPayment.TotalSalesTax)
                   );

                   pOSPayment.QrCodeURL = getQrCodeURL + ".png";
                }
            }

            var quote = abstractPOSPaymentServices.POSPayment_Upsert(pOSPayment);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //POSPayment_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSPayment_ById")]
        public async Task<IHttpActionResult> POSPayment_ById(int Id)
        {
            var quote = abstractPOSPaymentServices.POSPayment_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //PosInvoice_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("PosInvoice_ById")]
        public async Task<IHttpActionResult> PosInvoice_ById(int Id)
        {
            var quote = abstractPOSPaymentServices.PosInvoice_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //PosReturnInvoice_ById Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("PosReturnInvoice_ById")]
        public async Task<IHttpActionResult> PosReturnInvoice_ById(int Id)
        {
            var quote = abstractPOSPaymentServices.PosReturnInvoice_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //PosInvoice_All Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("PosInvoice_All")]
        public async Task<IHttpActionResult> PosInvoice_All(PageParam pageParam, string search = "", long SalonId = 0, string InvoiceDate = "", string InvoiceNumber = "", long CustomerId = 0)
        {
            var quote = abstractPOSPaymentServices.PosInvoice_All(pageParam, search, SalonId, InvoiceDate, InvoiceNumber, CustomerId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //PosReturnInvoice_Update Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("PosReturnInvoice_Update")]
        public async Task<IHttpActionResult> PosReturnInvoice_Update(PosReturnInvoice posReturnInvoice)
        {

            if (posReturnInvoice.SalonId > 0)
            {
                var response = abstractSalonsServices.Salons_ById(posReturnInvoice.SalonId);

                if (response.Item != null)
                {
                    var getQrCodeURL = GetQrCode(
                        response.Item.SalonName,
                        response.Item.TaxNumber,
                        ConvertTo.Double(posReturnInvoice.TotalAmount),
                        ConvertTo.Double(posReturnInvoice.TotalSalesTax)
                    );

                    posReturnInvoice.QrCodeURL = getQrCodeURL + ".png";
                }
            }

            var quote = abstractPOSPaymentServices.PosReturnInvoice_Update(posReturnInvoice);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        public string GetQrCode(string SellerName, string TaxNo, double TotalAmt, double TaxAmt)
        {
            DateTime DtVal = DateTime.UtcNow;
            string GenFile = GenerateQrCode(SellerName, TaxNo, DtVal, TotalAmt, TaxAmt);

            return GenFile;
        }

        public string GenerateQrCode(string SellerName, string TaxNo, DateTime DtTime, Double Total, Double Tax)
        {
            TLVCls tlv = new TLVCls(SellerName, TaxNo, DtTime, Total, Tax);
            System.Drawing.Image img = tlv.toQrCode();

            string fileName = "";

            if (img != null)
            {
                string basePath = "SimplifiedPOSInvoice/";
                fileName = DateTime.UtcNow.ToString("yyyyMMddhhmmssfff") + "_" + "QRCode";
                if (!Directory.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + basePath));
                }
                img.Save(HttpContext.Current.Server.MapPath("~/" + basePath + fileName + ".png"));
            }

            return fileName;
        }
    }
}
