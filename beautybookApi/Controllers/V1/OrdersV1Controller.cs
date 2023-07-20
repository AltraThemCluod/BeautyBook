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
using beautybookApi.PayTabs;
using Newtonsoft.Json;

namespace BeautyBookApi.Controllers.V1
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrdersV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractOrdersServices abstractOrdersServices;
        private readonly AbstractOrderStatusTrackingServices abstractOrderStatusTrackingServices;
        #endregion

        #region Cnstr
        public OrdersV1Controller(AbstractOrdersServices abstractOrdersServices, AbstractOrderStatusTrackingServices abstractOrderStatusTrackingServices)
        {
            this.abstractOrdersServices = abstractOrdersServices;
            this.abstractOrderStatusTrackingServices = abstractOrderStatusTrackingServices;
        }
        #endregion

        //Orders_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Orders_ById")]
        public async Task<IHttpActionResult> Orders_ById(long Id)
        {
            var quote = abstractOrdersServices.Orders_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Orders_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Orders_PaymentComplete")]
        public async Task<IHttpActionResult> Orders_PaymentComplete(long Id)
        {
            var quote = abstractOrdersServices.Orders_PaymentComplete(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Orders_ChangeStatus Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Orders_ChangeStatus")]
        public async Task<IHttpActionResult> Orders_ChangeStatus(long Id, long LookUpStatusId, string Comment)
        {
            var quote = abstractOrdersServices.Orders_ChangeStatus(Id, LookUpStatusId, Comment);
            if (quote.Item != null && quote.Code == 200)
            {
                AbstractOrderStatusTracking abstractOrderStatusTracking = new OrderStatusTracking();
                abstractOrderStatusTracking.OrderId = Id;
                abstractOrderStatusTracking.LookUpStatusId = LookUpStatusId;
                abstractOrderStatusTracking.Comment = Comment;

                abstractOrderStatusTrackingServices.OrderStatusTracking_Upsert(abstractOrderStatusTracking);
            }
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Orders_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Orders_Upsert")]
        public async Task<IHttpActionResult> Orders_Upsert(Orders orders)
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;
            orders.CreatedBy = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractOrdersServices.Orders_Upsert(orders);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Orders_UpdatePaymentMethod Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Orders_UpdatePaymentMethod")]
        public async Task<IHttpActionResult> Orders_UpdatePaymentMethod(Orders orders)
        {
            var quote = abstractOrdersServices.Orders_UpdatePaymentMethod(orders);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Orders_UpdateCheckoutDetails Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Orders_UpdateCheckoutDetails")]
        public async Task<IHttpActionResult> Orders_UpdateCheckoutDetails(Orders orders)
        {
            var quote = abstractOrdersServices.Orders_UpdateCheckoutDetails(orders);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        //Orders_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonOrder_All")]
        public async Task<IHttpActionResult> SalonOrder_All(PageParam pageParam, string search = "", long SalonId=0)
        {
            var quote = abstractOrdersServices.SalonOrder_All(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //SalonOrders_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Orders_All")]
        public async Task<IHttpActionResult> Orders_All(PageParam pageParam, string search = "", long LookUpStatusId = 0, string OrderNo = "", string DateOfOrder = "", long SalonId = 0, long VendorId = 0)
        {
            var quote = abstractOrdersServices.Orders_All(pageParam, search, LookUpStatusId, OrderNo, DateOfOrder, SalonId, VendorId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //Orders_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Orders_Delete")]
        public async Task<IHttpActionResult> Orders_Delete(long Id,long DeletedBy)
        {
            var quote = abstractOrdersServices.Orders_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Orders_PayTabsPayment")]
        public async Task<PaymentRequestModel> Orders_PayTabsPayment(decimal TotalAmount, string cart_id, string cart_description , string OrderId)
        {
            string strPayment = RestApis.Payment(TotalAmount,cart_id,cart_description , OrderId);
            PaymentRequestModel objmodel = JsonConvert.DeserializeObject<PaymentRequestModel>(strPayment);
            return objmodel;
        }


        //Invoice
        //OrderInvoiceMultipalVendor_All Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderInvoiceMultipalVendor_All")]
        public async Task<IHttpActionResult> OrderInvoiceMultipalVendor_All(PageParam pageParam, string search = "", long OrderId = 0)
        {
            var quote = abstractOrdersServices.OrderInvoiceMultipalVendor_All(pageParam, search, OrderId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //OrderInvoiceMultipalVendor_ById Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderInvoiceMultipalVendor_ById")]
        public async Task<IHttpActionResult> OrderInvoiceMultipalVendor_ById(long OrderId,long UserId)
        {
            var quote = abstractOrdersServices.OrderInvoiceMultipalVendor_ById(OrderId, UserId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //OrderInvoice_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderInvoice_All")]
        public async Task<IHttpActionResult> OrderInvoice_All(PageParam pageParam, string search = "", long UserId = 0, string InvoiceDate = "", string InvoiceNumber = "", long CustomerId = 0)
        {
            var quote = abstractOrdersServices.OrderInvoice_All(pageParam, search, UserId, InvoiceDate, InvoiceNumber, CustomerId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //OrderInvoiceSalon_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderInvoiceSalon_All")]
        public async Task<IHttpActionResult> OrderInvoiceSalon_All(PageParam pageParam, string search = "", long UserId = 0)
        {
            var quote = abstractOrdersServices.OrderInvoiceSalon_All(pageParam, search, UserId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //OrderInvoice_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderInvoice_ById")]
        public async Task<IHttpActionResult> OrderInvoice_ById(long Id)
        {
            var quote = abstractOrdersServices.OrderInvoice_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderReturnInvoice_ById")]
        public async Task<IHttpActionResult> OrderReturnInvoice_ById(long Id)
        {
            var quote = abstractOrdersServices.OrderReturnInvoice_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //OrderReturnInvoice_Update Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("OrderReturnInvoice_Update")]
        public async Task<IHttpActionResult> OrderReturnInvoice_Update(OrderReturnInvoice orderReturnInvoice)
        {
            var quote = abstractOrdersServices.OrderReturnInvoice_Update(orderReturnInvoice);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SalonOrderInvoice_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonOrderInvoice_All")]
        public async Task<IHttpActionResult> SalonOrderInvoice_All(PageParam pageParam, string search = "", long SalonId = 0, string InvoiceDate = "", string InvoiceNumber = "", long CustomerId = 0)
        {
            var quote = abstractOrdersServices.SalonOrderInvoice_All(pageParam, search, SalonId, InvoiceDate, InvoiceNumber, CustomerId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //SalonOrderVendor_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonOrderVendor_All")]
        public async Task<IHttpActionResult> SalonOrderVendor_All(PageParam pageParam, string search = "", long SalonId = 0)
        {
            var quote = abstractOrdersServices.SalonOrderVendor_All(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
