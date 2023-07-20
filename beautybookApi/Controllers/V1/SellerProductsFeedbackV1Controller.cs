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
    public class SellerProductsFeedbackV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractSellerProductsFeedbackServices abstractSellerProductsFeedbackServices;
        #endregion

        #region Cnstr
        public SellerProductsFeedbackV1Controller(AbstractSellerProductsFeedbackServices abstractSellerProductsFeedbackServices)
        {
            this.abstractSellerProductsFeedbackServices = abstractSellerProductsFeedbackServices;
        }
        #endregion

        //SellerProductsFeedback_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SellerProductsFeedback_ById")]
        public async Task<IHttpActionResult> SellerProductsFeedback_ById(long Id)
        {
            var quote = abstractSellerProductsFeedbackServices.SellerProductsFeedback_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SellerProductsFeedback_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SellerProductsFeedback_Upsert")]
        public async Task<IHttpActionResult> SellerProductsFeedback_Upsert(SellerProductsFeedback sellerProductsFeedback)
        {
            var quote = abstractSellerProductsFeedbackServices.SellerProductsFeedback_Upsert(sellerProductsFeedback);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
        //SellerProductsFeedback_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SellerProductsFeedback_All")]
        public async Task<IHttpActionResult> SellerProductsFeedback_All(PageParam pageParam, string search, AbstractSellerProductsFeedback abstractSellerProductsFeedback)
        {
            var quote = abstractSellerProductsFeedbackServices.SellerProductsFeedback_All(pageParam, search, abstractSellerProductsFeedback);
            return this.Content((HttpStatusCode)200, quote);
        }

        //SellerProductsFeedback_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SellerProductsFeedback_Delete")]
        public async Task<IHttpActionResult> SellerProductsFeedback_ChangeStatus(long Id, long LookUpStatusId, long LookUpStatusChangedBy)
        {
            var quote = abstractSellerProductsFeedbackServices.SellerProductsFeedback_ChangeStatus(Id, LookUpStatusId, LookUpStatusChangedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
        
    }
}
