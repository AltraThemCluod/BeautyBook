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
    public class MasterProductTypeV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractMasterProductTypeServices abstractMasterProductTypeServices;
        #endregion

        #region Cnstr
        public MasterProductTypeV1Controller(AbstractMasterProductTypeServices abstractMasterProductTypeServices)
        {
            this.abstractMasterProductTypeServices = abstractMasterProductTypeServices;
        }
        #endregion

        //MasterProductType_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterProductType_All")]
        public async Task<IHttpActionResult> MasterProductType_All(PageParam pageParam, string search, bool IsAllow = false)
        {
            var quote = abstractMasterProductTypeServices.MasterProductType_All(pageParam, search, IsAllow);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
