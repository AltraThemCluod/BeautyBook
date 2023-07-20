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
    public class EmailV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractCreate_EmailMsg_PackagesServices abstractCreate_EmailMsg_PackagesServices;
        #endregion

        #region Cnstr
        public EmailV1Controller(AbstractCreate_EmailMsg_PackagesServices abstractCreate_EmailMsg_PackagesServices)
        {
            this.abstractCreate_EmailMsg_PackagesServices = abstractCreate_EmailMsg_PackagesServices;
        }
        #endregion

        //Product_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmailPakages_All")]
        public async Task<IHttpActionResult> EmailPakages_All(PageParam pageParam, string search = "", long LookUpDurationId = 0,long SalonId = 0)
        {
            var quote = abstractCreate_EmailMsg_PackagesServices.Create_EmailMsg_Packages_All(pageParam, search, LookUpDurationId, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
