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
    public class EmailMarketingTemplatesV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractEmailMarketingTemplatesServices abstractEmailMarketingTemplatesServices;
        #endregion

        #region Cnstr
        public EmailMarketingTemplatesV1Controller(AbstractEmailMarketingTemplatesServices abstractEmailMarketingTemplatesServices)
        {
            this.abstractEmailMarketingTemplatesServices = abstractEmailMarketingTemplatesServices;
        }
        #endregion

        //EmailMarketingTemplates_All Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmailMarketingTemplates_All")]
        public async Task<IHttpActionResult> EmailMarketingTemplates_All(PageParam pageParam, string search)
        {
            var quote = abstractEmailMarketingTemplatesServices.EmailMarketingTemplates_All(pageParam, search);
            return this.Content((HttpStatusCode)200, quote);
        }

        //EmailMarketingTemplates_ById Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmailMarketingTemplates_ById")]
        public async Task<IHttpActionResult> EmailMarketingTemplates_ById(long Id)
        {
            var quote = abstractEmailMarketingTemplatesServices.EmailMarketingTemplates_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

    }
}
