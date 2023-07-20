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
    public class UserSalonsV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractUserSalonsServices abstractUserSalonsServices;
        #endregion

        #region Cnstr
        public UserSalonsV1Controller(AbstractUserSalonsServices abstractUserSalonsServices)
        {
            this.abstractUserSalonsServices = abstractUserSalonsServices;
        }
        #endregion

        //UserSalons_gettodayworksheet Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserSalons_gettodayworksheet")]
        public async Task<IHttpActionResult> UserSalons_gettodayworksheet(PageParam pageParam, string search = "", long UserId = 0, long SalonId = 0, string AttendanceDate = "")
        {
            var quote = abstractUserSalonsServices.UserSalons_gettodayworksheet(pageParam, search, UserId, SalonId, AttendanceDate);
            return this.Content((HttpStatusCode)200, quote);
        }

      
    }
}
