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
    public class POSOrderDetailsV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractPOSOrderDetailsServices abstractPOSOrderDetailsServices;
        #endregion

        #region Cnstr
        public POSOrderDetailsV1Controller(AbstractPOSOrderDetailsServices abstractPOSOrderDetailsServices)
        {
            this.abstractPOSOrderDetailsServices = abstractPOSOrderDetailsServices;
        }
        #endregion

        // POSOrderDetails_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSOrderDetails_Upsert")]
        public async Task<IHttpActionResult> POSOrderDetails_Upsert(POSOrderDetails pOSOrderDetails)
        {
            var quote = abstractPOSOrderDetailsServices.POSOrderDetails_Upsert(pOSOrderDetails);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //POSOrderDetails_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSOrderDetails_All")]
        public async Task<IHttpActionResult> POSOrderDetails_All(PageParam pageParam, string search = "")
        {
            var quote = abstractPOSOrderDetailsServices.POSOrderDetails_All(pageParam, search);
            return this.Content((HttpStatusCode)200, quote);
        }

        //POSOrderDetails_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSOrderDetails_ById")]
        public async Task<IHttpActionResult> POSOrderDetails_ById(int Id)
        {
            var quote = abstractPOSOrderDetailsServices.POSOrderDetails_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //POSOrderDetails_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("POSOrderDetails_Delete")]
        public async Task<IHttpActionResult> POSOrderDetails_Delete(int Id, int DeletedBy)
        {
            var quote = abstractPOSOrderDetailsServices.POSOrderDetails_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
