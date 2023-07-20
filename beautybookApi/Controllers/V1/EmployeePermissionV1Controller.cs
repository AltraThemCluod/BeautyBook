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
using Newtonsoft.Json;

namespace BeautyBookApi.Controllers.V1
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeePermissionV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractEmployeePermissionServices abstractEmployeePermissionServices;
        #endregion

        #region Cnstr
        public EmployeePermissionV1Controller(AbstractEmployeePermissionServices abstractEmployeePermissionServices)
        {
            this.abstractEmployeePermissionServices = abstractEmployeePermissionServices;
        }
        #endregion

        //EmployeeModulePermission_All Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmployeeModulePermission_All")]
        public async Task<IHttpActionResult> EmployeeModulePermission_All(PageParam pageParam, string search = "" , long EmployeeId = 0)
        {
            var quote = abstractEmployeePermissionServices.EmployeeModulePermission_All(pageParam, search , EmployeeId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //EmployeePermissionData_EmployeeId Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmployeePermissionData_EmployeeId")]
        public async Task<IHttpActionResult> EmployeePermissionData_EmployeeId(PageParam pageParam, string search = "" , long EmployeeId = 0 , long LookUpUserTypeId = 0)
        {
            var quote = abstractEmployeePermissionServices.EmployeePermissionData_EmployeeId(pageParam, search , EmployeeId , LookUpUserTypeId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //EmployeePermissionData_Upsert Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmployeePermissionData_Upsert")]
        public async Task<IHttpActionResult> EmployeePermissionData_Upsert(EmployeePermissionJsonData EmployeePermissionJsonData)
        {
            var quote = abstractEmployeePermissionServices.EmployeePermissionData_Upsert(EmployeePermissionJsonData);
            return this.Content((HttpStatusCode)200, quote);
        }

        //EmployeePermissionData_Delete Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmployeePermissionData_Delete")]
        public bool EmployeePermissionData_Delete(long EmployeeId)
        {
            var quote = abstractEmployeePermissionServices.EmployeePermissionData_Delete(EmployeeId);
            return quote;
        }
    }
}
