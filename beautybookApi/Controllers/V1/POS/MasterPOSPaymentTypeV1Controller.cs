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
    public class MasterPOSPaymentTypeV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractMasterPOSPaymentTypeServices abstractMasterPOSPaymentTypeServices;
        #endregion

        #region Cnstr
        public MasterPOSPaymentTypeV1Controller(AbstractMasterPOSPaymentTypeServices abstractMasterPOSPaymentTypeServices)
        {
            this.abstractMasterPOSPaymentTypeServices = abstractMasterPOSPaymentTypeServices;
        }
        #endregion


        //MasterPOSPaymentType_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterPOSPaymentType_All")]
        public async Task<IHttpActionResult> MasterPOSPaymentType_All(PageParam pageParam, string search = "")
        {
            var quote = abstractMasterPOSPaymentTypeServices.MasterPOSPaymentType_All(pageParam, search);
            return this.Content((HttpStatusCode)200, quote);
        }

    }
}
