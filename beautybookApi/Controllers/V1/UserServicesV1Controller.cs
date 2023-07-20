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
    public class UserServicesV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractUserServicesServices abstractUserServicesServices;
        #endregion

        #region Cnstr
        public UserServicesV1Controller(AbstractUserServicesServices abstractUserServicesServices)
        {
            this.abstractUserServicesServices = abstractUserServicesServices;
        }
        #endregion


        //UserServices_SalonServicesById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserServices_SalonServicesById")]
        public async Task<IHttpActionResult> UserServices_SalonServicesById(PageParam pageParam, string search, long SalonServicesById)
        {
            var quote = abstractUserServicesServices.UserServices_SalonServicesById(pageParam, search, SalonServicesById);
            return this.Content((HttpStatusCode)200, quote);
        }

        //UserServices_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserServices_Upsert")]
        public async Task<IHttpActionResult> UserServices_Upsert()
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;
            UserServices userServices = new UserServices();
            var httpRequest = HttpContext.Current.Request;
            userServices.UserId = Convert.ToInt64(httpRequest.Params["UserId"]);
            userServices.SalonServiceId = Convert.ToInt64(httpRequest.Params["SalonServiceId"]);
            userServices.Duration = Convert.ToString(httpRequest.Params["Duration"]);
            userServices.Price = Convert.ToDecimal(httpRequest.Params["Price"]);
            userServices.CreatedBy = Convert.ToInt64(httpRequest.Params["CreatedBy"]);

            var quote = abstractUserServicesServices.UserServices_Upsert(userServices);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //UserServices_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserServices_Delete")]
        public async Task<IHttpActionResult> UserServices_Delete(long Id, long DeletedBy)
        {
            var quote = abstractUserServicesServices.UserServices_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }




    }
}
