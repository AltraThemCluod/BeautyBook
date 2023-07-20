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
    public class LookUpLeaveTypeV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractLookUpLeaveTypeServices abstractLookUpLeaveTypeServices;
        #endregion

        #region Cnstr
        public LookUpLeaveTypeV1Controller(AbstractLookUpLeaveTypeServices abstractLookUpLeaveTypeServices)
        {
            this.abstractLookUpLeaveTypeServices = abstractLookUpLeaveTypeServices;
        }
        #endregion

        //LookUpLeaveType_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpLeaveType_All")]
        public async Task<IHttpActionResult> LookUpLeaveType_All(PageParam pageParam, string search)
        {
            var quote = abstractLookUpLeaveTypeServices.LookUpLeaveType_All(pageParam, search);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
