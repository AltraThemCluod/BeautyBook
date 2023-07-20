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
    public class PrivacyPolicyV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractPrivacyPolicyServices abstractPrivacyPolicyServices;
        #endregion

        #region Cnstr
        public PrivacyPolicyV1Controller(AbstractPrivacyPolicyServices abstractPrivacyPolicyServices)
        {
            this.abstractPrivacyPolicyServices = abstractPrivacyPolicyServices;
        }
        #endregion

        // PrivacyPolicy_ById Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("PrivacyPolicy_ById")]
        public async Task<IHttpActionResult> PrivacyPolicy_ById(long Id , long Type)
        {
            var quote = abstractPrivacyPolicyServices.PrivacyPolicy_ById(Id , Type);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
