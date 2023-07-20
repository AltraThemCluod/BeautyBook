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
    public class WriteaReviewV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractWriteaReviewServices abstractWriteaReviewServices;
        #endregion

        #region Cnstr
        public WriteaReviewV1Controller(AbstractWriteaReviewServices abstractWriteaReviewServices)
        {
            this.abstractWriteaReviewServices = abstractWriteaReviewServices;
        }
        #endregion

        
        //WriteaReview_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("WriteaReview_Upsert")]
        public async Task<IHttpActionResult> WriteaReview_Upsert(WriteaReview WriteaReview)
        {
              var quote = abstractWriteaReviewServices.WriteaReview_Upsert(WriteaReview);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //WriteaReview_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("WriteaReview_All")]
        public async Task<IHttpActionResult> WriteaReview_All(PageParam pageParam, string search,long ProductId,long VendorId)
        {
            var quote = abstractWriteaReviewServices.WriteaReview_All(pageParam, search, ProductId, VendorId);
            return this.Content((HttpStatusCode)200, quote);
        }
    }
}
