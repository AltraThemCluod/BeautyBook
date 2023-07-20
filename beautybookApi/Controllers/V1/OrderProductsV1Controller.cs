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
    public class OrderProductsV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractOrderProductsServices abstractOrderProductsServices;
        #endregion

        #region Cnstr
        public OrderProductsV1Controller(AbstractOrderProductsServices abstractOrderProductsServices)
        {
            this.abstractOrderProductsServices = abstractOrderProductsServices;
        }
        #endregion


        //OrderProducts_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderProducts_Upsert")]
        public async Task<IHttpActionResult> OrderProducts_Upsert(OrderProducts orderproducts)
        {
            var quote = abstractOrderProductsServices.OrderProducts_Upsert(orderproducts);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        //OrderProducts_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderProducts_All")]
        public async Task<IHttpActionResult> OrderProducts_All(PageParam pageParam, string search = "", long OrderId = 0,long VendorId = 0)
        {
            var quote = abstractOrderProductsServices.OrderProducts_All(pageParam, search, OrderId, VendorId);
            return this.Content((HttpStatusCode)200, quote);
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderProducts_Delete")]
        public async Task<IHttpActionResult> OrderProducts_Delete(long Id)
        {
            var quote = abstractOrderProductsServices.OrderProducts_Delete(Id);
            return this.Content((HttpStatusCode)200, quote);
        }


        //SalonOwnerOrdersCount_VendorId Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonOwnerOrdersCount_VendorId")]
        public async Task<IHttpActionResult> SalonOwnerOrdersCount_VendorId(long VendorId)
        {
            var quote = abstractOrderProductsServices.SalonOwnerOrdersCount_VendorId(VendorId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SalonOwnerOrdersCount_UpdateStatus Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonOwnerOrdersCount_UpdateStatus")]
        public async Task<IHttpActionResult> SalonOwnerOrdersCount_UpdateStatus(long OrderId)
        {
            var quote = abstractOrderProductsServices.SalonOwnerOrdersCount_UpdateStatus(OrderId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
