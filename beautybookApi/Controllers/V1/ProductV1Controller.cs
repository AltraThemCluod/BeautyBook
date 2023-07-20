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
    public class ProductV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractProductServices abstractProductServices;
        private readonly AbstractProductImageServices abstractProductImageServices;
        #endregion

        #region Cnstr
        public ProductV1Controller(AbstractProductServices abstractProductServices,
            AbstractProductImageServices abstractProductImageServices)
        {
            this.abstractProductServices = abstractProductServices;
            this.abstractProductImageServices = abstractProductImageServices;
        }
        #endregion

        //Product_ChangeStatus Api
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [InheritedRoute("Product_ChangeStatus")]
        public async Task<IHttpActionResult> Product_ChangeStatus(long Id, long LookUpStatusId)
        {
            var quote = abstractProductServices.Product_ChangeStatus(Id, LookUpStatusId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Product_UpdateQty Api
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [InheritedRoute("Product_UpdateQty")]
        public async Task<IHttpActionResult> Product_UpdateQty(long Id, long ProductQty, long UpdatedBy)
        {
            var quote = abstractProductServices.Product_UpdateQty(Id, ProductQty, UpdatedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Product_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Product_ById")]
        public async Task<IHttpActionResult> Product_ById(long Id)
        {
            var quote = abstractProductServices.Product_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Product_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Product_Upsert")]
        public async Task<IHttpActionResult> Product_Upsert()
        {

            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            Product Product = new Product();
            var httpRequest = HttpContext.Current.Request;
            Product.Id = Convert.ToInt64(httpRequest.Params["Id"]);
            Product.VendorId = Convert.ToInt64(httpRequest.Params["VendorId"]);
            Product.ProductName = Convert.ToString(httpRequest.Params["ProductName"]);
            Product.ProductTypeId = Convert.ToInt64(httpRequest.Params["ProductTypeId"]);
            Product.ProductCategoryId = Convert.ToInt64(httpRequest.Params["ProductCategoryId"]);
            Product.ProductBrandId = Convert.ToInt64(httpRequest.Params["ProductBrandId"]);
            Product.ProductQty = Convert.ToInt64(httpRequest.Params["ProductQty"]);
            Product.Price = Convert.ToDecimal(httpRequest.Params["Price"]);
            Product.Weight = Convert.ToDecimal(httpRequest.Params["Weight"]);
            Product.WeightTypeId = Convert.ToInt64(httpRequest.Params["WeightTypeId"]);
            Product.LowQtyAlert = Convert.ToInt64(httpRequest.Params["LowQtyAlert"]);
            Product.CreatedBy = Convert.ToInt64(httpRequest.Params["CreatedBy"]);
            Product.UpdatedBy = Convert.ToInt64(httpRequest.Params["UpdatedBy"]);
            Product.ProductTax = Convert.ToInt64(httpRequest.Params["ProductTax"]);
            Product.ProductInformation = Convert.ToString(httpRequest.Params["ProductInformation"]);
            Product.ReturnPolicy = Convert.ToString(httpRequest.Params["ReturnPolicy"]);
            Product.HighlightsLabel = Convert.ToString(httpRequest.Params["HighlightsLabel"]);
            Product.HighlightsDiscription = Convert.ToString(httpRequest.Params["HighlightsDiscription"]);
            Product.SpecificationsHighlightsLabel = Convert.ToString(httpRequest.Params["SpecificationsHighlightsLabel"]);
            Product.SpecificationsHighlightsDiscription = Convert.ToString(httpRequest.Params["SpecificationsHighlightsDiscription"]);
            Product.ServiceHighlightsDiscription = Convert.ToString(httpRequest.Params["ServiceHighlightsDiscription"]);
            Product.ExpiryDate = Convert.ToString(httpRequest.Params["ExpiryDate"]);
            Product.IsExpiryDate = Convert.ToBoolean(httpRequest.Params["IsExpiryDate"]);

            

            var quote = abstractProductServices.Product_Upsert(Product);
            if (httpRequest.Files.Count > 0 && quote.Item != null && quote.Code == 200)
            {
                
                string ProductImageIds = Convert.ToString(httpRequest.Params["ProductImageIds"]);

                string[] ProductImageIdArr = null;
                if (ProductImageIds != "")
                {
                    ProductImageIdArr = ProductImageIds.Split(',');
                }
                for (int i =0;i< httpRequest.Files.Count;i++)
                {
                    var myFile = httpRequest.Files[i];
                    string basePath = "ProductImage/" + quote.Item.Id + "/";
                    string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + Path.GetFileName(myFile.FileName);
                    if (!Directory.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/" + basePath))))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + basePath));
                    }
                    myFile.SaveAs(HttpContext.Current.Server.MapPath("~/" + basePath + fileName));

                    AbstractProductImage model = new ProductImage();
                    if (ProductImageIdArr != null && ProductImageIdArr[i] != null)
                    {
                        model.Id = Convert.ToInt64(ProductImageIdArr[i]);
                    }
                    
                    
                    model.ProductId = quote.Item.Id;
                    model.ImageUrl = basePath + fileName;
                    model.CreatedBy = Convert.ToInt64(httpRequest.Params["CreatedBy"]);
                    var response = abstractProductImageServices.ProductImage_Upsert(model);
                    if (response.Code != 200)
                    {
                        quote.Code = 400;
                        quote.Message = response.Message;
                        quote.Item = null;
                        break;
                    }
                    
                    
                }
                
            }
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Product_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Product_All")]
        public async Task<IHttpActionResult> Product_All(PageParam pageParam, string search, string ProductName, long ProductTypeId, long ProductBrandId, long LookUpStatusId, long VendorId, string ProductBrandIds, string ProductCategoryIds, string MinPrice, string MaxPrice,long SortBy = 4)
        {
            var quote = abstractProductServices.Product_All(pageParam, search, ProductName, ProductTypeId, ProductBrandId, LookUpStatusId, VendorId, ProductBrandIds, ProductCategoryIds, MinPrice, MaxPrice, SortBy);
            return this.Content((HttpStatusCode)200, quote);
        }

        //Product_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Product_Delete")]
        public async Task<IHttpActionResult> Product_Delete(long Id, long DeletedBy)
        {
            var quote = abstractProductServices.Product_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
