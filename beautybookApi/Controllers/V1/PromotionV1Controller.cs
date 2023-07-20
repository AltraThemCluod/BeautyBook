using BeautyBook.APICommon;
using BeautyBookApi.Models;
using BeautyBook.Common;
using RealEstateApi;
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
namespace RealEstateApi.Controllers.V1
{
    public class PromotionV1Controller : AbstractBaseController 
    {
        #region Fields
        private readonly AbstractPromotionServices abstractPromotionServices;
        #endregion

        #region Cnstr
        public PromotionV1Controller(AbstractPromotionServices abstractPromotionServices)
        {
            this.abstractPromotionServices = abstractPromotionServices;
        }
        #endregion


        [System.Web.Http.HttpPost]
        [InheritedRoute("Promotion_All")]
        public async Task<IHttpActionResult> Promotion_All(PageParam pageParam, string search = "", int VendorId = 0, int ProductId = 0, int ProductTypeId = 0, int ProductBrandId = 0)
        {
            var quote = abstractPromotionServices.Promotion_All(pageParam, search, VendorId, ProductId, ProductTypeId, ProductBrandId);
            return this.Content((HttpStatusCode)200, quote);
        }


        //Promotion_ById Api

        [System.Web.Http.HttpPost]
        [InheritedRoute("Promotion_ById")]
        public async Task<IHttpActionResult> Promotion_ById(int Id)
        {
            var quote = abstractPromotionServices.Promotion_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        // Promotion_Upsert Api

        [System.Web.Http.HttpPost]
        [InheritedRoute("Promotion_Upsert")]
        public async Task<IHttpActionResult> Promotion_Upsert(Promotion promotion)
        {
            var quote = abstractPromotionServices.Promotion_Upsert(promotion);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}