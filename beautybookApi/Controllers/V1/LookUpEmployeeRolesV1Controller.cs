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
    public class LookUpEmployeeRolesV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractLookUpEmployeeRolesServices abstractLookUpEmployeeRolesServices;
        #endregion

        #region Cnstr
        public LookUpEmployeeRolesV1Controller(AbstractLookUpEmployeeRolesServices abstractLookUpEmployeeRolesServices)
        {
            this.abstractLookUpEmployeeRolesServices = abstractLookUpEmployeeRolesServices;
        }
        #endregion

        //LookUpEmployeeRoles_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("LookUpEmployeeRoles_All")]
        public async Task<IHttpActionResult> LookUpEmployeeRoles_All(PageParam pageParam, string search,string IsLanguage)
        {
            var quote = abstractLookUpEmployeeRolesServices.LookUpEmployeeRoles_All(pageParam, search, IsLanguage);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
