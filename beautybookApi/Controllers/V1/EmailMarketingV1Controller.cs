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
    public class EmailMarketingV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractEmailMarketingServices abstractEmailMarketingServices;
        #endregion

        #region Cnstr
        public EmailMarketingV1Controller(AbstractEmailMarketingServices abstractEmailMarketingServices)
        {
            this.abstractEmailMarketingServices = abstractEmailMarketingServices;
        }
        #endregion

        //EmailMarketing_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmailMarketing_ById")]
        public async Task<IHttpActionResult> EmailMarketing_ById(long Id)
        {
            var quote = abstractEmailMarketingServices.EmailMarketing_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

       
        //EmailMarketing_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmailMarketing_Upsert")]
        public async Task<IHttpActionResult> EmailMarketing_Upsert(EmailMarketing emailMarketing)
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;
            emailMarketing.CreatedBy = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractEmailMarketingServices.EmailMarketing_Upsert(emailMarketing , 0);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        //EmailMarketing_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmailMarketing_All")]
        public async Task<IHttpActionResult> EmailMarketing_All(PageParam pageParam, string search, long CreatedBy,long SalonId)
        {
            var quote = abstractEmailMarketingServices.EmailMarketing_All(pageParam, search, CreatedBy, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        

    }
}
