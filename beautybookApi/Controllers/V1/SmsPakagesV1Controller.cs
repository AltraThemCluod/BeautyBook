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
    public class SmsV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractCreate_SMS_PackagesServices abstractCreate_SMS_PackagesServices;
        private readonly AbstractDashboardDataServices abstractDashboardDataServices;
        #endregion

        #region Cnstr
        public SmsV1Controller(AbstractCreate_SMS_PackagesServices abstractCreate_SMS_PackagesServices, AbstractDashboardDataServices abstractDashboardDataServices)
        {
            this.abstractCreate_SMS_PackagesServices = abstractCreate_SMS_PackagesServices;
            this.abstractDashboardDataServices = abstractDashboardDataServices;
        }
        #endregion

        //Product_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SmsPakages_All")]
        public async Task<IHttpActionResult> SmsPakages_All(PageParam pageParam, string search, long LookUpDurationId,long SalonId)
        {
            var quote = abstractCreate_SMS_PackagesServices.Create_SMS_Packages_All(pageParam, search, LookUpDurationId, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }


        //DashboardData_All Api
        [System.Web.Http.HttpPost]
        [InheritedRoute("DashboardData_All")]
        public async Task<IHttpActionResult> DashboardData_All(PageParam pageParam, string search, long LookUpCountry, long LookUpState)
        {
            var quote = abstractDashboardDataServices.DashboardData_All(pageParam, search, LookUpCountry, LookUpState);
            return this.Content((HttpStatusCode)200, quote);
        }

    }
}
