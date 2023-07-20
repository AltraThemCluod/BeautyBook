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
    public class UserWorkSheetV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractUserWorkSheetServices abstractUserWorkSheetServices;
        #endregion

        #region Cnstr
        public UserWorkSheetV1Controller(AbstractUserWorkSheetServices abstractUserWorkSheetServices)
        {
            this.abstractUserWorkSheetServices = abstractUserWorkSheetServices;
        }
        #endregion

        //UserWorkSheet_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserWorkSheet_ById")]
        public async Task<IHttpActionResult> UserWorkSheet_ById(long Id)
        {
            var quote = abstractUserWorkSheetServices.UserWorkSheet_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
        
        //UserWorkSheet_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserWorkSheet_Upsert")]
        public async Task<IHttpActionResult> UserWorkSheet_Upsert(UserWorkSheet UserWorkSheet)
        {
              var quote = abstractUserWorkSheetServices.UserWorkSheet_Upsert(UserWorkSheet);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        //UserWorkSheet_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserWorkSheet_All")]
        public async Task<IHttpActionResult> UserWorkSheet_All(PageParam pageParam, string search = "", long UserId = 0, long SalonId = 0, string AttendanceDate = "", long LookUpStatusId = 0, long LookUpEmployeeRolesId = 0)
        {

            AbstractUserWorkSheet UserWorkSheet = new UserWorkSheet();
            UserWorkSheet.UserId = UserId;
            UserWorkSheet.SalonId = SalonId;
            UserWorkSheet.AttendanceDate = AttendanceDate;
            UserWorkSheet.LookUpStatusId = LookUpStatusId;
            UserWorkSheet.LookUpEmployeeRolesId = LookUpEmployeeRolesId;
            var quote = abstractUserWorkSheetServices.UserWorkSheet_All(pageParam, search, UserWorkSheet);
            return this.Content((HttpStatusCode)200, quote);
        }

      
    }
}
