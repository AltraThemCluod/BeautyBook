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
    public class OfflineVendorV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractOfflineVendorServices abstractOfflineVendorServices;
        #endregion

        #region Cnstr
        public OfflineVendorV1Controller(AbstractOfflineVendorServices abstractOfflineVendorServices)
        {
            this.abstractOfflineVendorServices = abstractOfflineVendorServices;
        }
        #endregion


        //OfflineVendor_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OfflineVendor_All")]
        public async Task<IHttpActionResult> OfflineVendor_All(PageParam pageParam, string search = "",long SalonId=0)
        {
            var quote = abstractOfflineVendorServices.OfflineVendor_All(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }


        // OfflineVendor_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OfflineVendor_Upsert")]
        public async Task<IHttpActionResult> OfflineVendor_Upsert(OfflineVendor OfflineVendor)
        {
            var quote = abstractOfflineVendorServices.OfflineVendor_Upsert(OfflineVendor);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        // OfflineVendor_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OfflineVendor_ById")]
        public async Task<IHttpActionResult> OfflineVendor_ById(long Id)
        {
            var quote = abstractOfflineVendorServices.OfflineVendor_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

    }
}
