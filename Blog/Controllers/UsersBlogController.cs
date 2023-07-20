using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using Blog.Infrastructure;
using DataTables.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static Blog.Infrastructure.Enums;

namespace Blog.Controllers
{
    public class UsersBlogController : BaseController
    {

        #region Fields
        private readonly AbstractBlogServices abstractBlogServices;
        private readonly AbstractBlogCategoryServices abstractBlogCategoryServices;
        private readonly AbstractBlogContentServices abstractBlogContentServices;
        #endregion

        #region Ctor
        public UsersBlogController(AbstractBlogServices abstractBlogServices, 
            AbstractBlogCategoryServices abstractBlogCategoryServices,
            AbstractBlogContentServices abstractBlogContentServices)
        {
            this.abstractBlogServices = abstractBlogServices;
            this.abstractBlogCategoryServices = abstractBlogCategoryServices;
            this.abstractBlogContentServices = abstractBlogContentServices;
        }
        #endregion

        public ActionResult Index()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];
            return View();
        }

        public ActionResult Details(string ri = "MA==")
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            ViewBag.MasterCategory = MasterCategory();
            ViewBag.riid = ri;


            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(ri));

            ViewBag.DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(ri));

            var model = abstractBlogServices.Blog_ById(DecodeId);

            return View(model.Item);
        }

        public ActionResult ViewDetails(string ri = "MA==")
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            ViewBag.riid = ri;

            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(ri));

            ViewBag.DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(ri));

            ViewBag.model = abstractBlogServices.Blog_ById(DecodeId);

            return View();
        }

        public ActionResult ImageGallery()
        {
            if (TempData["openPopup"] != null)
                ViewBag.openPopup = TempData["openPopup"];

            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 10000;
            var model = abstractBlogServices.BlogImages_All(pageParam,"",(int)ProjectSession.AdminId);
            ViewBag.ImageModel = model;

            return View();
        }

        /// <summary>
        /// this function use to store category data in viewbag
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> MasterCategory()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;

                var model = abstractBlogCategoryServices.BlogCategory_All(pageParam,"");

                foreach (var master in model.Values)
                {
                    items.Add(new SelectListItem() { Text = master.CategoryName.ToString(), Value = Convert.ToString(master.Id) });
                }
                return items;
            }
            catch (Exception)
            {
                return items;
            }
        }

        /// <summary>
        /// This method use to listing blog data
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ManageBlogData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractBlogServices.Blog_All(pageParam, search, (int)ProjectSession.AdminId , 0);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// This method use to active and in-active blog data
        /// </summary>
        /// <param name="Id">Blog Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ActiveInActiveBlog(string Id = "MA==")
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(Id));
            SuccessResult<AbstractBlog> result = abstractBlogServices.Blog_ActInAct(DecodeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method use to delete blog data
        /// </summary>
        /// <param name="Id">Blog Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteBlog(string Id = "MA==")
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(Id));
            SuccessResult<AbstractBlog> result = abstractBlogServices.Blog_Delete(DecodeId);
            return Json(result, JsonRequestBehavior.AllowGet);
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

        /// <summary>
        ///  this methos use to create and update blog details
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="ProfileUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBlogDetails(UsersBlog usersBlog, HttpPostedFileBase ThumbnailImageUrl)
        {

            if (ThumbnailImageUrl != null)
            {
                try
                {
                    var base64Data = Regex.Match(usersBlog.ThumbnailDataUrl, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                    byte[] imageBytes = Convert.FromBase64String(base64Data);

                    //string filename = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                    //string path = Path.Combine(Server.MapPath("~/Blog/ThumbnailImage/"), filename + ".jpg");

                    string basePath = "Blog/ThumbnailImage/Crop/";
                    string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_Crop_" + ThumbnailImageUrl.FileName;
                    string path = Path.Combine(Server.MapPath("~/" + basePath), fileName);
                    if (!Directory.Exists(Path.Combine(HttpContext.Server.MapPath("~/" + basePath))))
                    {
                        Directory.CreateDirectory(HttpContext.Server.MapPath("~/" + basePath));
                    }

                    usersBlog.ThumbnailDataUrl = basePath + fileName;

                    System.IO.File.WriteAllBytes(path, imageBytes);
                }
                catch (Exception ex)
                {

                }
            }

            usersBlog.CreatedUserId = (int)ProjectSession.AdminId;
            if (ThumbnailImageUrl != null)
            {
                string basePath = "Blog/ThumbnailImage/";
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + Path.GetFileName(ThumbnailImageUrl.FileName);
                string path = Server.MapPath("~/" + basePath);
                if (!Directory.Exists(Path.Combine(HttpContext.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Server.MapPath("~/" + basePath));
                }
                ThumbnailImageUrl.SaveAs(HttpContext.Server.MapPath("~/" + basePath + fileName));
                usersBlog.ThumbnailImageUrl = basePath + fileName;
            }

            SuccessResult<AbstractBlog> result = abstractBlogServices.Blog_Upsert(usersBlog);

            if(result.Code == 200 && usersBlog.BlogContentDetailsData != null && usersBlog.BlogContentDetailsData != "")
            {
                List<BlogContentRoot> BlogContentResponse = JsonConvert.DeserializeObject<List<BlogContentRoot>>(usersBlog.BlogContentDetailsData);
                if(BlogContentResponse != null)
                {
                    for (int i=0; i < BlogContentResponse.Count; i++)
                    {
                        BlogContent blogContent = new BlogContent();
                        blogContent.Id = BlogContentResponse[i].Id;
                        blogContent.BlogId = result.Item.Id;
                        blogContent.Heading = BlogContentResponse[i].Heading;
                        blogContent.ImageUrl = BlogContentResponse[i].ImageUrl;
                        blogContent.Content = BlogContentResponse[i].Content;

                        abstractBlogContentServices.BlogContent_Upsert(blogContent);
                    }
                }
            }

            if (result.Item == null)
            {
                TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.error.ToString());
                return RedirectToAction("Index", "UsersBlog", new { Area = "" });
            }
            TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.success.ToString());
            return RedirectToAction("Index", "UsersBlog", new { Area = "" });
        }

        /// <summary>
        /// This method use in upload images
        /// </summary>
        /// <param name="blogImages"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadBlogImage(HttpPostedFileBase ImageUrl)
        {
            BlogImages blogImages = new BlogImages();
            blogImages.UserId = (int)ProjectSession.AdminId;

            if (ImageUrl != null)
            {
                string basePath = "Blog/Gallery/";
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + Path.GetFileName(ImageUrl.FileName);
                string path = Server.MapPath("~/" + basePath);
                if (!Directory.Exists(Path.Combine(HttpContext.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Server.MapPath("~/" + basePath));
                }
                ImageUrl.SaveAs(HttpContext.Server.MapPath("~/" + basePath + fileName));
                blogImages.ImageUrl = basePath + fileName;
            }

            SuccessResult<AbstractBlogImages> result = abstractBlogServices.BlogImages_Upsert(blogImages);
            if (result.Item == null)
            {
                TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.error.ToString());
                return RedirectToAction("ImageGallery", "UsersBlog", new { Area = "" });
            }
            TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.success.ToString());
            return RedirectToAction("ImageGallery", "UsersBlog", new { Area = "" });
        }


        /// <summary>
        /// This method use to delete blog images data
        /// </summary>
        /// <param name="Id">blog image Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteBlogImage(string Id = "MA==")
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(Id));
            SuccessResult<AbstractBlogImages> result = abstractBlogServices.BlogImages_Delete(DecodeId);
            if (result.Item == null)
            {
                TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.error.ToString());
            }
            TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.success.ToString());
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method use to delete blog content data
        /// </summary>
        /// <param name="Id">Blog Content Id</param>
        /// <returns></returns>
        [HttpDelete]
        public JsonResult DeleteBlogContent(string Id = "MA==")
        {
            int DecodeId = Convert.ToInt32(ConvertTo.Base64Decode(Id));
            SuccessResult<AbstractBlogContent> result = abstractBlogContentServices.BlogContent_Delete(DecodeId);
            if (result.Item == null)
            {
                TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.error.ToString());
            }
            TempData["openPopup"] = CommonHelper.ShowNotifyMessage(result.Message, MessageType.success.ToString());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}