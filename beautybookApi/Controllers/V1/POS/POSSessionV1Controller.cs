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
    public class POSSessionV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractPOSSessionServices abstractPOSSessionServices;
        #endregion

        #region Cnstr
        public POSSessionV1Controller(AbstractPOSSessionServices abstractPOSSessionServices)
        {
            this.abstractPOSSessionServices = abstractPOSSessionServices;
        }
        #endregion

        // POSSession_Create Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSSession_Create")]
        public async Task<IHttpActionResult> POSSession_Create(long Id, long SalonId, bool IsNewSessionCreate , string CoinsAmountStr = "")
        {
            var quote = abstractPOSSessionServices.POSSession_Create(Id, SalonId, IsNewSessionCreate , CoinsAmountStr);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //POSSession_BySalonId Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSSession_BySalonId")]
        public async Task<IHttpActionResult> POSSession_BySalonId(PageParam pageParam, string search = "" , long SalonId = 0)
        {
            var quote = abstractPOSSessionServices.POSSession_BySalonId(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //POSSession_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSSession_ById")]
        public async Task<IHttpActionResult> POSSession_ById(long Id)
        {
            var quote = abstractPOSSessionServices.POSSession_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //POSSession_TopBySalonId Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSSession_TopBySalonId")]
        public async Task<IHttpActionResult> POSSession_TopBySalonId(long SalonId)
        {
            var quote = abstractPOSSessionServices.POSSession_TopBySalonId(SalonId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //POSSession_Authentication Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSSession_Authentication")]
        public async Task<IHttpActionResult> POSSession_Authentication(string Email , long SessionId)
        {
            var quote = abstractPOSSessionServices.POSSession_Authentication(Email, SessionId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //POSSessionClosing_CachAndAt Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSSessionClosing_CachAndAt")]
        public async Task<IHttpActionResult> POSSessionClosing_CachAndAt(long Id)
        {
            var quote = abstractPOSSessionServices.POSSessionClosing_CachAndAt(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
