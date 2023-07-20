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
    public class DailyDealsV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractDailyDealsServices abstractDailyDealsServices;
        #endregion

        #region Cnstr
        public DailyDealsV1Controller(AbstractDailyDealsServices abstractDailyDealsServices)
        {
            this.abstractDailyDealsServices = abstractDailyDealsServices;
        }
        #endregion


        //DailyDeals_All Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("DailyDeals_All")]
        public async Task<IHttpActionResult> DailyDeals_All(PageParam pageParam,string search = "",long SalonId = 0)
        {
            var quote = abstractDailyDealsServices.DailyDeals_All(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        // DailyDeals_Upsert Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("DailyDeals_Upsert")]
        public async Task<IHttpActionResult> DailyDeals_Upsert(DailyDeals DailyDeals)
        {
            var quote = abstractDailyDealsServices.DailyDeals_Upsert(DailyDeals);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        // DailyDeals_ById Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("DailyDeals_ById")]
        public async Task<IHttpActionResult> DailyDeals_ById(long Id)
        {
            var quote = abstractDailyDealsServices.DailyDeals_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        // DailyDeals_Delete Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("DailyDeals_Delete")]
        public async Task<IHttpActionResult> DailyDeals_Delete(long Id, long DeletedBy)
        {
            var quote = abstractDailyDealsServices.DailyDeals_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        

    }
}
