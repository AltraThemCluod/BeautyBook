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
    public class TermsAndConditionsV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractTermsAndConditionsServices abstractTermsAndConditionsServices;
        #endregion

        #region Cnstr
        public TermsAndConditionsV1Controller(AbstractTermsAndConditionsServices abstractTermsAndConditionsServices)
        {
            this.abstractTermsAndConditionsServices = abstractTermsAndConditionsServices;
        }
        #endregion

        //TermsAndConditions_ById Api
        [System.Web.Http.HttpPost]
        [InheritedRoute("TermsAndConditions_ById")]
        public async Task<IHttpActionResult> TermsAndConditions_ById(long Id)
        {
            var quote = abstractTermsAndConditionsServices.TermsAndConditions_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}