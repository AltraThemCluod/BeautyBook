using BeautyBook.APICommon;
using BeautyBook.Common;
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
namespace MRAPI.Controllers.V1
{
    public class ProductWishlistV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractProductWishlistServices abstractProductWishlistServices;
        #endregion

        #region Cnstr
        public ProductWishlistV1Controller(AbstractProductWishlistServices abstractProductWishlistServices)
        {
            this.abstractProductWishlistServices = abstractProductWishlistServices;
        }
        #endregion


        //ProductWishlist_All Api

        [System.Web.Http.HttpPost]
        [InheritedRoute("ProductWishlist_All")]
        public async Task<IHttpActionResult> ProductWishlist_All(PageParam pageParam, string search = "",long SalonId = 0 )
        {
            var quote = abstractProductWishlistServices.ProductWishlist_All(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }




        // ProductWishlist_Delete Api

        [System.Web.Http.HttpPost]
        [InheritedRoute("ProductWishlist_Delete")]
        public async Task<IHttpActionResult> ProductWishlist_Delete(long Id, long DeletedBy)
        {
            var quote = abstractProductWishlistServices.ProductWishlist_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        // ProductWishlist_Upsert Api

        [System.Web.Http.HttpPost]
        [InheritedRoute("ProductWishlist_Upsert")]
        public async Task<IHttpActionResult> ProductWishlist_Upsert(ProductWishlist productWishlist)
        {
            var quote = abstractProductWishlistServices.ProductWishlist_Upsert(productWishlist);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }




    }
}
