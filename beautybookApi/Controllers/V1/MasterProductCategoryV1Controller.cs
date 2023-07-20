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
    public class MasterProductCategoryV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractMasterProductCategoryServices abstractMasterProductCategoryServices;
        #endregion

        #region Cnstr
        public MasterProductCategoryV1Controller(AbstractMasterProductCategoryServices abstractMasterProductCategoryServices)
        {
            this.abstractMasterProductCategoryServices = abstractMasterProductCategoryServices;
        }
        #endregion


        //MasterProductCategory_All Api

        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterProductCategory_All")]
        public async Task<IHttpActionResult> MasterProductCategory_All(PageParam pageParam, string search = "", int ProductTypeId = 0)
        {
            var quote = abstractMasterProductCategoryServices.MasterProductCategory_All(pageParam, search, ProductTypeId);
            return this.Content((HttpStatusCode)200, quote);
        }


        //MasterProductCategory_ById Api

        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterProductCategory_ById")]
        public async Task<IHttpActionResult> MasterProductCategory_ById(int Id)
        {
            var quote = abstractMasterProductCategoryServices.MasterProductCategory_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        // MasterProductCategory_Upsert Api

        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterProductCategory_Upsert")]
        public async Task<IHttpActionResult> MasterProductCategory_Upsert(MasterProductCategory MasterProductCategory)
        {
            var quote = abstractMasterProductCategoryServices.MasterProductCategory_Upsert(MasterProductCategory);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        // MasterProductCategory_Delete Api

        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterProductCategory_Delete")]
        public async Task<IHttpActionResult> MasterProductCategory_Delete(int Id, int DeletedBy)
        {
            var quote = abstractMasterProductCategoryServices.MasterProductCategory_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        // MasterProductCategory_ActInact Api

        [System.Web.Http.HttpPost]
        [InheritedRoute("MasterProductCategory_ActInAct")]
        public async Task<IHttpActionResult> MasterProductCategory_ActInact(int Id)
        {
            var quote = abstractMasterProductCategoryServices.MasterProductCategory_ActInact(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

    }
}
