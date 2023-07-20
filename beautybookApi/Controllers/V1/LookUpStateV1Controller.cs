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
    public class LookUpStateV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractLookUpStateServices abstractLookUpStateServices;
        #endregion

        #region Cnstr
        public LookUpStateV1Controller(AbstractLookUpStateServices abstractLookUpStateServices)
        {
            this.abstractLookUpStateServices = abstractLookUpStateServices;
        }
        #endregion

        //LookUpState_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpState_All")]
        public async Task<IHttpActionResult> LookUpState_All(PageParam pageParam, string search, long CountryId)
        {
            var quote = abstractLookUpStateServices.LookUpState_All(pageParam, search, CountryId);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
