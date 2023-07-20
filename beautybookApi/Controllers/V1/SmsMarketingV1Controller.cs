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
    public class SmsMarketingV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractSmsMarketingServices abstractSmsMarketingServices;
        #endregion

        #region Cnstr
        public SmsMarketingV1Controller(AbstractSmsMarketingServices abstractSmsMarketingServices)
        {
            this.abstractSmsMarketingServices = abstractSmsMarketingServices;
        }
        #endregion

        //SmsMarketing_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SmsMarketing_ById")]
        public async Task<IHttpActionResult> SmsMarketing_ById(long Id)
        {
            var quote = abstractSmsMarketingServices.SmsMarketing_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SmsMarketing_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SmsMarketing_Upsert")]
        public async Task<IHttpActionResult> SmsMarketing_Upsert(SmsMarketing smsMarketing)
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;
            smsMarketing.CreatedBy = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractSmsMarketingServices.SmsMarketing_Upsert(smsMarketing, 0);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SmsMarketing_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SmsMarketing_All")]
        public async Task<IHttpActionResult> SmsMarketing_All(PageParam pageParam, string search, long CreatedBy,long SalonId)
        {
            var quote = abstractSmsMarketingServices.SmsMarketing_All(pageParam, search, CreatedBy, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        

    }
}
