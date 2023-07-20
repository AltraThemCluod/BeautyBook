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
    public class LookUpEmployeeTypeV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractLookUpEmployeeTypeServices abstractLookUpEmployeeTypeServices;
        #endregion

        #region Cnstr
        public LookUpEmployeeTypeV1Controller(AbstractLookUpEmployeeTypeServices abstractLookUpEmployeeTypeServices)
        {
            this.abstractLookUpEmployeeTypeServices = abstractLookUpEmployeeTypeServices;
        }
        #endregion

        //LookUpEmployeeType_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpEmployeeType_All")]
        public async Task<IHttpActionResult> LookUpEmployeeType_All(PageParam pageParam, string search)
        {
            var quote = abstractLookUpEmployeeTypeServices.LookUpEmployeeType_All(pageParam, search);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
