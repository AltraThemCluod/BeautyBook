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
    public class TechnicalSupportV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractTechnicalSupportServices abstractTechnicalSupportServices;
        #endregion

        #region Cnstr
        public TechnicalSupportV1Controller(AbstractTechnicalSupportServices abstractTechnicalSupportServices)
        {
            this.abstractTechnicalSupportServices = abstractTechnicalSupportServices;
        }
        #endregion


        //TechnicalSupport_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("TechnicalSupport_All")]
        public async Task<IHttpActionResult> TechnicalSupport_All(PageParam pageParam, string search = "",long SalonId=0,long UserId = 0,long UserTypeId = 0)
        {
            var quote = abstractTechnicalSupportServices.TechnicalSupport_All(pageParam, search, SalonId, UserId, UserTypeId);
            return this.Content((HttpStatusCode)200, quote);
        }

        // TechnicalSupport_Upsert Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("TechnicalSupport_Upsert")]
        public async Task<IHttpActionResult> TechnicalSupport_Upsert(TechnicalSupport TechnicalSupport)
        {
            var quote = abstractTechnicalSupportServices.TechnicalSupport_Upsert(TechnicalSupport);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        // TechnicalSupport_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("TechnicalSupport_Delete")]
        public async Task<IHttpActionResult> TechnicalSupport_Delete(long Id, long DeletedBy)
        {
            var quote = abstractTechnicalSupportServices.TechnicalSupport_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        // TechnicalSupport_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("TechnicalSupport_ById")]
        public async Task<IHttpActionResult> TechnicalSupport_ById(long Id)
        {
            var quote = abstractTechnicalSupportServices.TechnicalSupport_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
