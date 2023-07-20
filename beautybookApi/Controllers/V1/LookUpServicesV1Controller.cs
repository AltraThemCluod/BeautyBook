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
    public class LookUpServicesV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractLookUpServicesServices abstractLookUpServicesServices;
        #endregion

        #region Cnstr
        public LookUpServicesV1Controller(AbstractLookUpServicesServices abstractLookUpServicesServices)
        {
            this.abstractLookUpServicesServices = abstractLookUpServicesServices;
        }
        #endregion

        //LookUpServices_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpServices_ById")]
        public async Task<IHttpActionResult> LookUpServices_ById()
        {

            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            long Id = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractLookUpServicesServices.LookUpServices_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //LookUpServices_Upsert Api
        //[System.Web.Http.Authorize]
        //[System.Web.Http.HttpPost]
        //[InheritedRoute("LookUpServices_Upsert")]
        //public async Task<IHttpActionResult> LookUpServices_Upsert(LookUpServices lookUpServices)
        //{
        //    var quote = abstractLookUpServicesServices.LookUpServices_Upsert(lookUpServices);
        //    return this.Content((HttpStatusCode)quote.Code, quote);
        //}


        //LookUpServices_Upsert Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpServices_Upsert")]
        public async Task<IHttpActionResult> LookUpServices_Upsert()
        {
            //var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            LookUpServices lookUpServices = new LookUpServices();
            var httpRequest = HttpContext.Current.Request;
            lookUpServices.Id = Convert.ToInt64(httpRequest.Params["Id"]);
            lookUpServices.Name = Convert.ToString(httpRequest.Params["Name"]);
            lookUpServices.ParentId = Convert.ToInt64(httpRequest.Params["ParentId"]);
            lookUpServices.SalonId = Convert.ToInt64(httpRequest.Params["SalonId"]);
            lookUpServices.UserId = Convert.ToInt64(httpRequest.Params["UserId"]);
            lookUpServices.Price = Convert.ToDecimal(httpRequest.Params["Price"]);
            lookUpServices.Duration = Convert.ToDecimal(httpRequest.Params["Duration"]);
            lookUpServices.IconUrl = Convert.ToString(httpRequest.Params["IconUrl"]);
            lookUpServices.ServPhotoUrl = Convert.ToString(httpRequest.Params["ServPhotoUrl"]);
            lookUpServices.CreatedBy = Convert.ToInt64(httpRequest.Params["CreatedBy"]);
			lookUpServices.UpdatedBy = Convert.ToInt64(httpRequest.Params["UpdatedBy"]);


            if (httpRequest.Files.Count > 0)
            {
                var myFile = httpRequest.Files[0];
                string basePath = "ServPhoto/" + lookUpServices.Id + "/";
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + Path.GetFileName(myFile.FileName);
                if (!Directory.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + basePath));
                }
                myFile.SaveAs(HttpContext.Current.Server.MapPath("~/" + basePath + fileName));
                lookUpServices.ServPhotoUrl = basePath + fileName;
            }

            var quote = abstractLookUpServicesServices.LookUpServices_Upsert(lookUpServices);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //LookUpServices_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpServices_All")]
        public async Task<IHttpActionResult> LookUpServices_All(PageParam pageParam, string search , long ParentId,long SalonId)
        {

            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            long UserId = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractLookUpServicesServices.LookUpServices_All(pageParam, search, ParentId, SalonId, UserId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //CustomCategory_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("CustomCategory_All")]
        public async Task<IHttpActionResult> CustomCategory_All(PageParam pageParam, string search, long ParentId, long SalonId)
        {

            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            long UserId = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractLookUpServicesServices.CustomCategory_All(pageParam, search, ParentId, SalonId, UserId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //CustomServices_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("CustomServices_All")]
        public async Task<IHttpActionResult> CustomServices_All(PageParam pageParam, string search, long SalonId)
        {

            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            long UserId = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractLookUpServicesServices.CustomServices_All(pageParam, search, SalonId, UserId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //LookUpServices_ParentId Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpServices_ParentId")]
        public async Task<IHttpActionResult> LookUpServices_ParentId(PageParam pageParam, string search, AbstractLookUpServices abstractLookUpServices)
        {
            var quote = abstractLookUpServicesServices.LookUpServices_ParentId(pageParam, search, abstractLookUpServices);
            return this.Content((HttpStatusCode)200, quote);
        }
        //LookUpServices_ActInact Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpServices_ActInact")]
        public async Task<IHttpActionResult> LookUpServices_ActInact(long Id,long LookUpStatusId,long LookUpStatusChangedBy)
        {
            var quote = abstractLookUpServicesServices.LookUpServices_ActInact(Id, LookUpStatusId, LookUpStatusChangedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //LookUpServices_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpServices_Delete")]
        public async Task<IHttpActionResult> LookUpServices_Delete(long Id,long DeletedBy)
        {
            var quote = abstractLookUpServicesServices.LookUpServices_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
        //LookUpServices_AllServices Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpServices_AllServices")]
        public async Task<IHttpActionResult> LookUpServices_AllServices(long SalonId = 0,long PackageId = 0)
        {
            var quote = abstractLookUpServicesServices.LookUpServices_AllServices(SalonId, PackageId);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
