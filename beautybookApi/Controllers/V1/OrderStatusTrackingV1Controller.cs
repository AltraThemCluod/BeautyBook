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
    public class OrderStatusTrackingV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractOrderStatusTrackingServices abstractOrderStatusTrackingServices;
        #endregion

        #region Cnstr
        public OrderStatusTrackingV1Controller(AbstractOrderStatusTrackingServices abstractOrderStatusTrackingServices)
        {
            this.abstractOrderStatusTrackingServices = abstractOrderStatusTrackingServices;
        }
        #endregion

        //OrderStatusTracking_ByOrderId Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderStatusTracking_ByOrderId")]
        public async Task<IHttpActionResult> OrderStatusTracking_ByOrderId(long OrderId)
        {
            var quote = abstractOrderStatusTrackingServices.OrderStatusTracking_ByOrderId(OrderId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
        
        //OrderStatusTracking_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderStatusTracking_Upsert")]
        public async Task<IHttpActionResult> OrderStatusTracking_Upsert(OrderStatusTracking orderStatusTracking)
        {
              var quote = abstractOrderStatusTrackingServices.OrderStatusTracking_Upsert(orderStatusTracking);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
