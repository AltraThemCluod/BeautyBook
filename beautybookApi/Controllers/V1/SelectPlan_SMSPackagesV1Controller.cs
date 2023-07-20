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
    public class SelectPlan_SMSPackagesV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractSelectPlan_SMSPackagesServices abstractSelectPlan_SMSPackagesServices;
        #endregion

        #region Cnstr
        public SelectPlan_SMSPackagesV1Controller(AbstractSelectPlan_SMSPackagesServices abstractSelectPlan_SMSPackagesServices)
        {
            this.abstractSelectPlan_SMSPackagesServices = abstractSelectPlan_SMSPackagesServices;
        }
        #endregion

        //SelectPlan_SMSPackages_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SelectPlan_SMSPackages_Upsert")]
        public async Task<IHttpActionResult> SelectPlan_SMSPackages_Upsert(SelectPlan_SMSPackages selectPlan_SMSPackages)
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;
            selectPlan_SMSPackages.CreatedBy = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractSelectPlan_SMSPackagesServices.SelectPlan_SMSPackages_Upsert(selectPlan_SMSPackages);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SelectPlanSMSPackages_UpdatePaymentStatus Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SelectPlanSMSPackages_UpdatePaymentStatus")]
        public async Task<IHttpActionResult> SelectPlanSMSPackages_UpdatePaymentStatus(long SelectPlanSMSPackagesId)
        {
            var quote = abstractSelectPlan_SMSPackagesServices.SelectPlanSMSPackages_UpdatePaymentStatus(SelectPlanSMSPackagesId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        [System.Web.Http.HttpPost]
        [InheritedRoute("Payment_SMSPackages")]
        public async Task<PaymentRequestModel> Payment_SMSPackages(decimal TotalAmount, string cart_id, string cart_description, string PackageId)
        {
            string strPayment = RestApis.PaymentSMSPackages(TotalAmount, cart_id, cart_description, PackageId);
            PaymentRequestModel objmodel = JsonConvert.DeserializeObject<PaymentRequestModel>(strPayment);
            return objmodel;
        }

        //SelectPlan_SMSPackages_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SelectPlan_SMSPackages_BySalonId")]
        public async Task<IHttpActionResult> SelectPlan_SMSPackages_BySalonId(PageParam pageParam, string search, long SalonId)
        {
            var quote = abstractSelectPlan_SMSPackagesServices.SelectPlan_SMSPackages_BySalonId(pageParam, search,SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
