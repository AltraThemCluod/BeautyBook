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
    public class AddToCartV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractAddToCartServices abstractAddToCartServices;
        #endregion

        #region Cnstr
        public AddToCartV1Controller(AbstractAddToCartServices abstractAddToCartServices)
        {
            this.abstractAddToCartServices = abstractAddToCartServices;
        }
        #endregion


        //AddToCart_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("AddToCart_All")]
        public async Task<IHttpActionResult> AddToCart_All(PageParam pageParam, long SalonId=0, string search = "")
        {
            var quote = abstractAddToCartServices.AddToCart_All(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //AddToCart_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("AddToCart_Upsert")]
        public async Task<IHttpActionResult> AddToCart_Upsert(AddToCart AddToCart)
        {
            var quote = abstractAddToCartServices.AddToCart_Upsert(AddToCart);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        // AddToCart_UpdateQty Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("AddToCart_UpdateQty")]
        public async Task<IHttpActionResult> AddToCart_UpdateQty(string ProductIds, string Qtys)
        {
            var quote = abstractAddToCartServices.AddToCart_UpdateQty(ProductIds, Qtys);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        // AddToCart_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("AddToCart_Delete")]
        public async Task<IHttpActionResult> AddToCart_Delete(long Id, long DeletedBy)
        {
            var quote = abstractAddToCartServices.AddToCart_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        

    }
}
