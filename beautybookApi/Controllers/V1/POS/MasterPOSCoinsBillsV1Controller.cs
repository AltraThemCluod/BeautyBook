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
    public class MasterPOSCoinsBillsV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractMasterPOSCoinsBillsServices abstractMasterPOSCoinsBillsServices;
        #endregion

        #region Cnstr
        public MasterPOSCoinsBillsV1Controller(AbstractMasterPOSCoinsBillsServices abstractMasterPOSCoinsBillsServices)
        {
            this.abstractMasterPOSCoinsBillsServices = abstractMasterPOSCoinsBillsServices;
        }
        #endregion


        //MasterPOSCoinsBills_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterPOSCoinsBills_All")]
        public async Task<IHttpActionResult> MasterPOSCoinsBills_All(PageParam pageParam, string search = "")
        {
            var quote = abstractMasterPOSCoinsBillsServices.MasterPOSCoinsBills_All(pageParam, search);
            return this.Content((HttpStatusCode)200, quote);
        }

    }
}
