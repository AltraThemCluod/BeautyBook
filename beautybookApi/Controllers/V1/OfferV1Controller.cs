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
using System.Web.Http.Cors;

namespace BeautyBookApi.Controllers.V1
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OfferV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractOfferServices abstractOfferServices;
        #endregion

        #region Cnstr
        public OfferV1Controller(AbstractOfferServices abstractOfferServices)
        {
            this.abstractOfferServices = abstractOfferServices;
        }
        #endregion

        //Offer_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Offer_All")]
        public async Task<IHttpActionResult> Offer_All(PageParam pageParam, string search = "",long CreatedBy = 0,long SalonId = 0)
        {
            var quote = abstractOfferServices.Offer_All(pageParam, search, CreatedBy,SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }


        //Offer_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Offer_ById")]
        public async Task<IHttpActionResult> Offer_ById(long Id)
        {
            var quote = abstractOfferServices.Offer_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        // Offer_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Offer_Upsert")]
        public async Task<IHttpActionResult> Offer_Upsert(Offer Offer)
        {
            var quote = abstractOfferServices.Offer_Upsert(Offer);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        // Offer_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Offer_Delete")]
        public async Task<IHttpActionResult> Offer_Delete(long Id, long DeletedBy)
        {
            var quote = abstractOfferServices.Offer_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

    }
}
