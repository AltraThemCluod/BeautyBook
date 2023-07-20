using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
//using PublicZaanVendor.Common;
//using PublicZaanVendor.Common.Paging;
//using PublicZaanVendor.Data.V1;
//using PublicZaanVendor.Entities.Contract;
//using PublicZaanVendor.Entities.V1;
using PublicZaanVendor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicZaanVendor.Controllers
{
    public class BlogController : BaseController
    {
        #region Fields
        private readonly AbstractBlogServices abstractBlogServices;
        private readonly AbstractBlogCategoryServices abstractBlogCategoryServices;
        private readonly AbstractBlogSubscribeNewslatterServices abstractBlogSubscribeNewslatterServices;
        #endregion

        #region Ctor
        public BlogController(AbstractBlogServices abstractBlogServices,
            AbstractBlogCategoryServices abstractBlogCategoryServices,
            AbstractBlogSubscribeNewslatterServices abstractBlogSubscribeNewslatterServices)
        {
            this.abstractBlogServices = abstractBlogServices;
            this.abstractBlogCategoryServices = abstractBlogCategoryServices;
            this.abstractBlogSubscribeNewslatterServices = abstractBlogSubscribeNewslatterServices;
        }
        #endregion

        //BlogCategoryDao abstractBlogCategoryServices = new BlogCategoryDao();
        //BlogDao abstractBlogServices = new BlogDao();
        //BlogSubscribeNewslatterDao abstractBlogSubscribeNewslatterServices = new BlogSubscribeNewslatterDao();


        [Route("blog")]
        public ActionResult Index()
        {
            ViewBag.BlogResponse = abstractBlogServices.BlogTopPost_All();
            ViewBag.BlogTopArticleResponse = abstractBlogServices.BlogTopArticles_All();

            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 100;

            ViewBag.BlogCategory = abstractBlogCategoryServices.BlogCategory_All(pageParam, "");

            return View();
        }

        [Route("blog-inner")]
        public ActionResult BlogInner(string ri = "MA==")
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            ViewBag.riid = ri;
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            ViewBag.DecodeId = DecodeId;
            ViewBag.BlogInnerResponse = abstractBlogServices.Blog_ById(DecodeId);

            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 100;

            if(ViewBag.BlogInnerResponse.IsValid == true)
            {
                ViewBag.BlogCategoryresponse = abstractBlogServices.Blog_All(pageParam, "", 0, ViewBag.BlogInnerResponse.Item.CategoryId);
            }

            return View();
        }

        [HttpGet]
        public JsonResult GetCategoryBlog(int CategoryId = 0 , int Offset=0)
        {
            PageParam pageParam = new PageParam();
            pageParam.Offset = Offset;
            pageParam.Limit=10;

            var response = abstractBlogServices.Blog_All(pageParam,"",0,CategoryId);

            return Json(response , JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method use to get blog content data
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult BlogContentData(string Id = "MA==")
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(Id));
            SuccessResult<AbstractBlog> result = abstractBlogServices.Blog_ById(DecodeId);
            return Json(result.Item.BlogContent, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSubscribeNewslatter(BlogSubscribeNewslatter blogSubscribeNewslatter)
        {

            SuccessResult<AbstractBlogSubscribeNewslatter> result = abstractBlogSubscribeNewslatterServices.BlogSubscribeNewslatter_Insert(blogSubscribeNewslatter);
            if (result.Item == null)
            {
                TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, "error");
                return RedirectToAction("BlogInner", "Blog", new { Area = "" , @ri = @ConvertTo.Base64Encode(Convert.ToString(blogSubscribeNewslatter.BlogId)) });
            }
            TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, "success");
            return RedirectToAction("BlogInner", "Blog", new { Area = "", @ri = @ConvertTo.Base64Encode(Convert.ToString(blogSubscribeNewslatter.BlogId)) });
        }
        
        /// <summary>
        /// add and remove blog like's
        /// </summary>
        /// <param name="BlogId"></param>
        /// <param name="IsLike"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddBlogLike(string bi , bool IsLike)
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(bi));
            var response = abstractBlogServices.BlogLike_Insert(DecodeId, IsLike);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddSocialShare(string bi,int Type)
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(bi));
            var response = abstractBlogServices.SocialShare_Insert(DecodeId, Type);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}