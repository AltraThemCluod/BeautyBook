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
    public class UserWorkingHoursV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractUserWorkingHoursServices abstractUserWorkingHoursServices;
        #endregion

        #region Cnstr
        public UserWorkingHoursV1Controller(AbstractUserWorkingHoursServices abstractUserWorkingHoursServices)
        {
            this.abstractUserWorkingHoursServices = abstractUserWorkingHoursServices;
        }
        #endregion


        //UserWorkingHours_SalonServicesById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserWorkingHours_ByUserId")]
        public async Task<IHttpActionResult> UserWorkingHours_ByUserId(PageParam pageParam, string search ,long UserId)
        {
            var quote = abstractUserWorkingHoursServices.UserWorkingHours_ByUserId(pageParam, search, UserId);
            return this.Content((HttpStatusCode)200, quote);
        }


        //EmployeeWorkingHour_ServicesId Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("EmployeeWorkingHour_ServicesId")]
        public async Task<IHttpActionResult> EmployeeWorkingHour_ServicesId(PageParam pageParam, string search, long CategoryId, string ServicesIds, long SalonId)
        {
            var quote = abstractUserWorkingHoursServices.EmployeeWorkingHour_ServicesId(pageParam, search, CategoryId, ServicesIds, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }


    }
}
