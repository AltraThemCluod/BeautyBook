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
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PackageCustomPriceV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractPackageCustomPriceServices abstractPackageCustomPriceServices;
        #endregion

        #region Cnstr
        public PackageCustomPriceV1Controller(AbstractPackageCustomPriceServices abstractPackageCustomPriceServices)
        {
            this.abstractPackageCustomPriceServices = abstractPackageCustomPriceServices;
        }
        #endregion

        // PackageCustomPrice_Create Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("PackageCustomPrice_Create")]
        public async Task<IHttpActionResult> PackageCustomPrice_Create(PackageCustomPrice PackageCustomPrice)
        {
            var quote = abstractPackageCustomPriceServices.PackageCustomPrice_Create(PackageCustomPrice);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


    }
}
