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
    public class MasterProductBrandV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractMasterProductBrandServices abstractMasterProductBrandServices;
        #endregion

        #region Cnstr
        public MasterProductBrandV1Controller(AbstractMasterProductBrandServices abstractMasterProductBrandServices)
        {
            this.abstractMasterProductBrandServices = abstractMasterProductBrandServices;
        }
        #endregion

        //MasterProductBrand_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterProductBrand_All")]
        public async Task<IHttpActionResult> MasterProductBrand_All(PageParam pageParam, string search,long SalonId)
        {
            var quote = abstractMasterProductBrandServices.MasterProductBrand_All(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }


        // MasterProductCategory_Upsert Api
        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterProductBrand_Upsert")]
        public async Task<IHttpActionResult> MasterProductBrand_Upsert(MasterProductBrand MasterProductBrand)
        {
            var quote = abstractMasterProductBrandServices.MasterProductBrand_Upsert(MasterProductBrand);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //MasterProductBrand_Delete Api
        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterProductBrand_Delete")]
        public async Task<IHttpActionResult> MasterProductBrand_Delete(long Id, long DeletedBy)
        {
            var quote = abstractMasterProductBrandServices.MasterProductBrand_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

    }
}
