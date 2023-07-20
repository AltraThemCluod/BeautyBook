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
    public class SelectPlan_EmailPackagesV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractSelectPlan_EmailPackagesServices abstractSelectPlan_EmailPackagesServices;
        #endregion

        #region Cnstr
        public SelectPlan_EmailPackagesV1Controller(AbstractSelectPlan_EmailPackagesServices abstractSelectPlan_EmailPackagesServices)
        {
            this.abstractSelectPlan_EmailPackagesServices = abstractSelectPlan_EmailPackagesServices;
        }
        #endregion


        //SelectPlan_EmailPackages_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SelectPlan_EmailPackages_BySalonId")]
        public async Task<IHttpActionResult> SelectPlan_EmailPackages_BySalonId(PageParam pageParam, long SalonId=0, string search = "")
        {
            var quote = abstractSelectPlan_EmailPackagesServices.SelectPlan_EmailPackages_BySalonId(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        // SelectPlan_EmailPackages_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SelectPlan_EmailPackages_Upsert")]
        public async Task<IHttpActionResult> SelectPlan_EmailPackages_Upsert(SelectPlan_EmailPackages SelectPlan_EmailPackages)
        {
            var quote = abstractSelectPlan_EmailPackagesServices.SelectPlan_EmailPackages_Upsert(SelectPlan_EmailPackages);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        [System.Web.Http.HttpPost]
        [InheritedRoute("Payment_EmailPackages")]
        public async Task<PaymentRequestModel> Payment_EmailPackages(decimal TotalAmount, string cart_id, string cart_description, string PackageId)
        {
            string strPayment = RestApis.PaymentEmailPackages(TotalAmount, cart_id, cart_description, PackageId);
            PaymentRequestModel objmodel = JsonConvert.DeserializeObject<PaymentRequestModel>(strPayment);
            return objmodel;
        }

        // SelectPlanEmailPackages_UpdatePaymentStatus Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SelectPlanEmailPackages_UpdatePaymentStatus")]
        public async Task<IHttpActionResult> SelectPlanEmailPackages_UpdatePaymentStatus(long SelectPlanEmailPackagesId)
        {
            var quote = abstractSelectPlan_EmailPackagesServices.SelectPlanEmailPackages_UpdatePaymentStatus(SelectPlanEmailPackagesId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

    }
}
