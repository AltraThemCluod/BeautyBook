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
    public class SalonVendorDashboardV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractSalonVendorDashboardServices abstractSalonVendorDashboardServices;
        #endregion

        #region Cnstr
        public SalonVendorDashboardV1Controller(AbstractSalonVendorDashboardServices abstractSalonVendorDashboardServices)
        {
            this.abstractSalonVendorDashboardServices = abstractSalonVendorDashboardServices;
        }
        #endregion



        // VendorSalesAndRating_VendorId Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("VendorSalesAndRating_VendorId")]
        public async Task<IHttpActionResult> VendorSalesAndRating_VendorId(long VendorId)
        {
            var quote = abstractSalonVendorDashboardServices.VendorSalesAndRating_VendorId(VendorId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //VendorTopCustomer_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("VendorTopCustomer_All")]
        public async Task<IHttpActionResult> VendorTopCustomer_All(PageParam pageParam,string search = "", long VendorId = 0, string FromDate = "",string ToDate = "",long Type = 0)
        {
            var quote = abstractSalonVendorDashboardServices.VendorTopCustomer_All(pageParam, search, VendorId, FromDate,ToDate,Type);
            return this.Content((HttpStatusCode)200, quote);
        }

        //VendorTopProduct_All Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("VendorTopProduct_All")]
        public async Task<IHttpActionResult> VendorTopProduct_All(PageParam pageParam, string search = "", long VendorId = 0, string FromDate = "", string ToDate = "", long Type = 0)
        {
            var quote = abstractSalonVendorDashboardServices.VendorTopProduct_All(pageParam, search, VendorId, FromDate, ToDate, Type);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
