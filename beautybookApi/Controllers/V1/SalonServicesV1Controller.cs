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
    public class SalonServicesV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractSalonServicesServices abstractSalonServicesServices;
        #endregion

        #region Cnstr
        public SalonServicesV1Controller(AbstractSalonServicesServices abstractSalonServicesServices)
        {
            this.abstractSalonServicesServices = abstractSalonServicesServices;
        }
        #endregion

        //SalonServices_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonServices_ById")]
        public async Task<IHttpActionResult> SalonServices_ById(long Id)
        {
            var quote = abstractSalonServicesServices.SalonServices_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SalonServices_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonServices_Upsert")]
        public async Task<IHttpActionResult> SalonServices_Upsert(SalonServices salonServices)
        {
            var quote = abstractSalonServicesServices.SalonServices_Upsert(salonServices);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //BlankSalonServices_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("BlankSalonServices_Upsert")]
        public async Task<IHttpActionResult> BlankSalonServices_Upsert(SalonServices salonServices)
        {
            var quote = abstractSalonServicesServices.BlankSalonServices_Upsert(salonServices);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SalonServices_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonServices_All")]
        public async Task<IHttpActionResult> SalonServices_All(PageParam pageParam, string search ,long SalonsId, long LookUpServicesId, long LookUpStatusId , long LookUpCategoryId)
        {
            var quote = abstractSalonServicesServices.SalonServices_All(pageParam, search, SalonsId , LookUpServicesId , LookUpStatusId, LookUpCategoryId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //SalonServices_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonServices_ActInact")]
        public async Task<IHttpActionResult> SalonServices_ActInact(long Id,long LookUpStatusId, long SalonId, long LookUpStatusChangedBy)
        {
            var quote = abstractSalonServicesServices.SalonServices_ActInact(Id, LookUpStatusId, SalonId, LookUpStatusChangedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SalonServices_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonServices_Delete")]
        public async Task<IHttpActionResult> SalonServices_Delete(long Id,long DeletedBy)
        {
            var quote = abstractSalonServicesServices.SalonServices_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
