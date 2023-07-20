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
    public class SendEmailMarketingTemplatesV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractSendEmailMarketingTemplatesServices abstractSendEmailMarketingTemplatesServices;
        #endregion

        #region Cnstr
        public SendEmailMarketingTemplatesV1Controller(AbstractSendEmailMarketingTemplatesServices abstractSendEmailMarketingTemplatesServices)
        {
            this.abstractSendEmailMarketingTemplatesServices = abstractSendEmailMarketingTemplatesServices;
        }
        #endregion

        //SendEmailMarketingTemplates_Upsert Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SendEmailMarketingTemplates_Upsert")]
        public async Task<IHttpActionResult> SendEmailMarketingTemplates_Upsert(SendEmailMarketingTemplates sendEmailMarketingTemplates)
        {
            var quote = abstractSendEmailMarketingTemplatesServices.SendEmailMarketingTemplates_Upsert(sendEmailMarketingTemplates);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //EmailUser_All Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmailUser_All")]
        public async Task<IHttpActionResult> EmailUser_All(PageParam pageParam, string search, string CustomerSinceStartDate, string CustomerSinceEndDate, long MinAppoinment, long MaxAppoinment, long MinAge, long MaxAge, string LastVisitStartDate, string LastVisitEndDate, string ServicesIds,long IsAllCustomer,long SalonId)
        {
            var quote = abstractSendEmailMarketingTemplatesServices.EmailUser_All(pageParam, search, CustomerSinceStartDate, CustomerSinceEndDate, MinAppoinment, MaxAppoinment, MinAge, MaxAge, LastVisitStartDate, LastVisitEndDate, ServicesIds,IsAllCustomer, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
