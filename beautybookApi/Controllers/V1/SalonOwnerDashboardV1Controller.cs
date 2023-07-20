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
    public class SalonOwnerDashboardV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractSalonOwnerDashboardServices abstractSalonOwnerDashboardServices;
        #endregion

        #region Cnstr
        public SalonOwnerDashboardV1Controller(AbstractSalonOwnerDashboardServices abstractSalonOwnerDashboardServices)
        {
            this.abstractSalonOwnerDashboardServices = abstractSalonOwnerDashboardServices;
        }
        #endregion

        // SalonOwnerSMSandEmailPackages_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonOwnerSMSandEmailPackages_All")]
        public async Task<IHttpActionResult> SalonOwnerSMSandEmailPackages_All(long SalonId)
        {
            var quote = abstractSalonOwnerDashboardServices.SalonOwnerSMSandEmailPackages_All(SalonId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        // TecnicalSupport_SalonId Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("TecnicalSupport_SalonId")]
        public async Task<IHttpActionResult> TecnicalSupport_SalonId(long SalonId)
        {
            var quote = abstractSalonOwnerDashboardServices.TecnicalSupport_SalonId(SalonId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SalonOwnerNewCustomer_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonOwnerNewCustomer_All")]
        public async Task<IHttpActionResult> SalonOwnerNewCustomer_All(PageParam pageParam,string search = "", long SalonId = 0, string FromDate = "",string ToDate = "",long Type = 0)
        {
            var quote = abstractSalonOwnerDashboardServices.SalonOwnerNewCustomer_All(pageParam, search, SalonId,FromDate,ToDate,Type);
            return this.Content((HttpStatusCode)200, quote);
        }

        //SalonOwnerMostRequestedEmployee_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonOwnerMostRequestedEmployee_All")]
        public async Task<IHttpActionResult> SalonOwnerMostRequestedEmployee_All(PageParam pageParam, string search = "", long SalonId = 0, string FromDate = "", string ToDate = "", long Type = 0)
        {
            var quote = abstractSalonOwnerDashboardServices.SalonOwnerMostRequestedEmployee_All(pageParam, search, SalonId, FromDate, ToDate,Type);
            return this.Content((HttpStatusCode)200, quote);
        }

        //SalonOwnerDashboard_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonOwnerDashboard_All")]
        public async Task<IHttpActionResult> SalonOwnerDashboard_All(PageParam pageParam, string search = "", long SalonId = 0, string FromDate = "", string ToDate = "",long Type = 0)
        {
            var quote = abstractSalonOwnerDashboardServices.SalonOwnerDashboard_All(pageParam, search, SalonId, FromDate, ToDate,Type);
            return this.Content((HttpStatusCode)200, quote);
        }

        //SalonOwnerTopRequestedServices_All Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonOwnerTopRequestedServices_All")]
        public async Task<IHttpActionResult> SalonOwnerTopRequestedServices_All(PageParam pageParam, string search = "", long SalonId = 0,string ServiceIds = "", string FromDate = "", string ToDate = "", long Type = 0)
        {
            var quote = abstractSalonOwnerDashboardServices.SalonOwnerTopRequestedServices_All(pageParam, search, SalonId, ServiceIds, FromDate, ToDate,Type);
            return this.Content((HttpStatusCode)200, quote);
        }



    }
}
