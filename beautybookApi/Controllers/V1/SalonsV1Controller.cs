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

namespace BeautyBookApi.Controllers.V1
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SalonsV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractSalonsServices abstractSalonsServices;
        private object httpRequest;
        #endregion

        #region Cnstr
        public SalonsV1Controller(AbstractSalonsServices abstractSalonsServices)
        {
            this.abstractSalonsServices = abstractSalonsServices;
        }
        #endregion

        //Salons_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Salons_ById")]
        public async Task<IHttpActionResult> Salons_ById(long Id)
        {
            var quote = abstractSalonsServices.Salons_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Salons_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Salons_Upsert")]
        public async Task<IHttpActionResult> Salons_Upsert()
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;
            long UserId = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            Salons salons = new Salons();
            var httpRequest = HttpContext.Current.Request;
            salons.Id = Convert.ToInt64(httpRequest.Params["Id"]);
            salons.UserId = UserId;
            salons.SalonName = Convert.ToString(httpRequest.Params["SalonName"]);
            salons.PrimaryPhone = Convert.ToString(httpRequest.Params["PrimaryPhone"]);
            salons.AlternatePhone = Convert.ToString(httpRequest.Params["AlternatePhone"]);
            salons.AddressLine1 = Convert.ToString(httpRequest.Params["AddressLine1"]);
            salons.Latitude = Convert.ToString(httpRequest.Params["Latitude"]);
            salons.Longitude = Convert.ToString(httpRequest.Params["Longitude"]);
            salons.AddressLine2 = Convert.ToString(httpRequest.Params["AddressLine2"]);
            salons.LookUpCountryId = Convert.ToInt64(httpRequest.Params["LookUpCountryId"]);
            salons.LookUpStateId = Convert.ToInt64(httpRequest.Params["LookUpStateId"]);
            salons.City = Convert.ToString(httpRequest.Params["City"]);
            salons.ZipCode = Convert.ToString(httpRequest.Params["ZipCode"]);
            salons.BankName = Convert.ToString(httpRequest.Params["BankName"]);
            salons.IBANNumber = Convert.ToString(httpRequest.Params["IBANNumber"]);
            salons.TaxNumber = Convert.ToString(httpRequest.Params["TaxNumber"]);
            salons.VATNumber = Convert.ToString(httpRequest.Params["VATNumber"]);

            if (httpRequest.Files.Count > 0)
            {
                var myFile = httpRequest.Files[0];
                string basePath = "SalonsLogo/" + salons.Id + "/";
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + Path.GetFileName(myFile.FileName);
                if (!Directory.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + basePath));
                }
                myFile.SaveAs(HttpContext.Current.Server.MapPath("~/" + basePath + fileName));
                salons.LogoUrl = basePath + fileName;
            }

            var quote = abstractSalonsServices.Salons_Upsert(salons);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Salons_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Salons_All")]
        public async Task<IHttpActionResult> Salons_All(PageParam pageParam, string search , long LookUpStatusId)
        {
            var quote = abstractSalonsServices.Salons_All(pageParam, search, LookUpStatusId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //Salons_ByUserId Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Salons_ByUserId")]
        public async Task<IHttpActionResult> Salons_ByUserId(PageParam pageParam, string search)
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;
            long UserId = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);
            long LookUpStatusId = 0;
            var quote = abstractSalonsServices.Salons_ByUserId(pageParam, search, UserId , LookUpStatusId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //Salons_ApprovedUnApproved Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Salons_ApprovedUnApproved")]
        public async Task<IHttpActionResult> Salons_ApprovedUnApproved(long Id,long LookUpStatusId,long LookUpStatusChangedBy)
        {
            var quote = abstractSalonsServices.Salons_ApprovedUnApproved(Id, LookUpStatusId, LookUpStatusChangedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Salons_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Salons_ActInact")]
        public async Task<IHttpActionResult> Salons_ActInact(long Id,long LookUpStatusId,long LookUpStatusChangedBy)
        {
            var quote = abstractSalonsServices.Salons_ActInact(Id,LookUpStatusId, LookUpStatusChangedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Salons_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Salons_Delete")]
        public async Task<IHttpActionResult> Salons_Delete(long Id,long DeletedBy)
        {
            var quote = abstractSalonsServices.Salons_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
