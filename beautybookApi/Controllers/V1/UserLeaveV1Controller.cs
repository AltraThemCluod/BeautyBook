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
    public class UserLeaveV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractUserLeaveServices abstractUserLeaveServices;
        #endregion

        #region Cnstr
        public UserLeaveV1Controller(AbstractUserLeaveServices abstractUserLeaveServices)
        {
            this.abstractUserLeaveServices = abstractUserLeaveServices;
        }
        #endregion

        //UserLeave_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserLeave_ById")]
        public async Task<IHttpActionResult> UserLeave_ById(long Id)
        {
            var quote = abstractUserLeaveServices.UserLeave_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //UserLeave_ChangeStatus Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserLeave_ChangeStatus")]
        public async Task<IHttpActionResult> UserLeave_ChangeStatus(long Id, long LookUpStatusId, long LookUpStatusChangedBy)
        {
            var quote = abstractUserLeaveServices.UserLeave_ChangeStatus(Id, LookUpStatusId, LookUpStatusChangedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }



        //UserLeave_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserLeave_Upsert")]
        public async Task<IHttpActionResult> UserLeave_Upsert(UserLeave userleave)
        {
              var quote = abstractUserLeaveServices.UserLeave_Upsert(userleave);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        //UserLeave_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserLeave_All")]
        public async Task<IHttpActionResult> UserLeave_All(PageParam pageParam, string search = "", long UserId = 0,long SalonId = 0, long LookUpLeaveTypeId = 0, string FromDate = "",string ToDate = "", long LookUpStatusId = 0, long LookUpEmployeeRolesId = 0)
        {

            AbstractUserLeave UserLeave = new UserLeave();
            UserLeave.UserId = UserId;
            UserLeave.SalonId = SalonId;
            UserLeave.LookUpLeaveTypeId = LookUpLeaveTypeId;
            UserLeave.FromDate = FromDate;
            UserLeave.ToDate = ToDate;
            UserLeave.LookUpStatusId = LookUpStatusId;
            UserLeave.LookUpEmployeeRolesId = LookUpEmployeeRolesId;
            var quote = abstractUserLeaveServices.UserLeave_All(pageParam, search, UserLeave);
            return this.Content((HttpStatusCode)200, quote);
        }

        //UserLeave_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserLeave_Delete")]
        public async Task<IHttpActionResult> UserLeave_Delete(long Id,long DeletedBy)
        {
            var quote = abstractUserLeaveServices.UserLeave_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //UserLeave_LeaveTypeCount Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserLeave_LeaveTypeCount")]
        public async Task<IHttpActionResult> UserLeave_LeaveTypeCount(PageParam pageParam, long UserId = 0, long SalonId = 0)
        {
            AbstractUserLeave UserLeave = new UserLeave();
            UserLeave.UserId = UserId;
            UserLeave.SalonId = SalonId;
            var quote = abstractUserLeaveServices.UserLeave_LeaveTypeCount(pageParam, UserLeave);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
