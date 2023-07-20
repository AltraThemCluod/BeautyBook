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
    public class POSDetailsV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractPOSDetailsServices abstractPOSDetailsServices;
        #endregion

        #region Cnstr
        public POSDetailsV1Controller(AbstractPOSDetailsServices abstractPOSDetailsServices)
        {
            this.abstractPOSDetailsServices = abstractPOSDetailsServices;
        }
        #endregion

        // POSDetails_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSDetails_Upsert")]
        public async Task<IHttpActionResult> POSDetails_Upsert(POSDetails pOSDetails)
        {
            var quote = abstractPOSDetailsServices.POSDetails_Upsert(pOSDetails);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //POSDetails_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSDetails_All")]
        public async Task<IHttpActionResult> POSDetails_All(PageParam pageParam, string search = "",long SalonId = 0,long POSSessionId = 0)
        {
            var quote = abstractPOSDetailsServices.POSDetails_All(pageParam, search, SalonId, POSSessionId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //POSAssignEmployee_All Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSAssignEmployee_All")]
        public async Task<IHttpActionResult> POSAssignEmployee_All(long LookUpUserTypeId, long SalonId)
        {
            var quote = abstractPOSDetailsServices.POSAssignEmployee_All(LookUpUserTypeId, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //PosOffer_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("PosOffer_All")]
        public async Task<IHttpActionResult> PosOffer_All(long OfferId = 0, long SalonId = 0)
        {
            var quote = abstractPOSDetailsServices.PosOffer_All(OfferId, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }


        //POSDetails_ById Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSDetails_ById")]
        public async Task<IHttpActionResult> POSDetails_ById(int Id)
        {
            var quote = abstractPOSDetailsServices.POSDetails_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //POSDetails_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSDetails_Delete")]
        public async Task<IHttpActionResult> POSDetails_Delete(int Id, int DeletedBy)
        {
            var quote = abstractPOSDetailsServices.POSDetails_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
