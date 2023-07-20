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
    public class MasterServicePackageV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractMasterServicePackageServices abstractMasterServicePackageServices;
        #endregion

        #region Cnstr
        public MasterServicePackageV1Controller(AbstractMasterServicePackageServices abstractMasterServicePackageServices)
        {
            this.abstractMasterServicePackageServices = abstractMasterServicePackageServices;
        }
        #endregion


        //MasterServicePackage_All Api

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterServicePackage_All")]
        public async Task<IHttpActionResult> MasterServicePackage_All(PageParam pageParam, string search = "",long ServicePackageId = 0)
        {
            var quote = abstractMasterServicePackageServices.MasterServicePackage_All(pageParam, search, ServicePackageId);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
