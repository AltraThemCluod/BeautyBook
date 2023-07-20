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
    public class ShippingAddressV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractShippingAddressServices abstractShippingAddressServices;
        #endregion

        #region Cnstr
        public ShippingAddressV1Controller(AbstractShippingAddressServices abstractShippingAddressServices)
        {
            this.abstractShippingAddressServices = abstractShippingAddressServices;
        }
        #endregion

        //ShippingAddress_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("ShippingAddress_Upsert")]
        public async Task<IHttpActionResult> ShippingAddress_Upsert(ShippingAddress shippingAddress)
        {
              var quote = abstractShippingAddressServices.ShippingAddress_Upsert(shippingAddress);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        //ShippingAddress_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("ShippingAddress_All")]
        public async Task<IHttpActionResult> ShippingAddress_All(PageParam pageParam, string search = "",long SalonId = 0)
        {
            var quote = abstractShippingAddressServices.ShippingAddress_All(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //ShippingAddress_Delete Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("ShippingAddress_Delete")]
        public async Task<IHttpActionResult> ShippingAddress_Delete(long Id,long DeletedBy)
        {
            var quote = abstractShippingAddressServices.ShippingAddress_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
        //ShippingAddress_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("ShippingAddress_ById")]
        public async Task<IHttpActionResult> ShippingAddress_ById(long Id)
        {
            var quote = abstractShippingAddressServices.ShippingAddress_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

    }
}
