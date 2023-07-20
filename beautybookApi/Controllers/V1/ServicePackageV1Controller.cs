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
    public class ServicePackageV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractServicePackageServices abstractServicePackageServices;
        #endregion

        #region Cnstr
        public ServicePackageV1Controller(AbstractServicePackageServices abstractServicePackageServices)
        {
            this.abstractServicePackageServices = abstractServicePackageServices;
        }
        #endregion


        //ServicePackage_All Api

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("ServicePackage_All")]
        public async Task<IHttpActionResult> ServicePackage_All(PageParam pageParam, string search = "",long SalonId = 0)
        {
            var quote = abstractServicePackageServices.ServicePackage_All(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }


        //ServicePackage_ById Api

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("ServicePackage_ById")]
        public async Task<IHttpActionResult> ServicePackage_ById(long Id)
        {
            var quote = abstractServicePackageServices.ServicePackage_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        // ServicePackage_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("ServicePackage_Upsert")]
        public async Task<IHttpActionResult> ServicePackage_Upsert(ServicePackage servicePackage)
        {
            var quote = abstractServicePackageServices.ServicePackage_Upsert(servicePackage);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        // ServicePackage_ActInAct Api

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("ServicePackage_ActInAct")]
        public async Task<IHttpActionResult> ServicePackage_ActInAct(long Id)
        {
            var quote = abstractServicePackageServices.ServicePackage_ActInAct(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

    }
}
