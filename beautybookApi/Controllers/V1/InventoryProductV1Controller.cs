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
    public class InventoryProductV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractInventoryProductServices abstractInventoryProductServices;
        #endregion

        #region Cnstr
        public InventoryProductV1Controller(AbstractInventoryProductServices abstractInventoryProductServices)
        {
            this.abstractInventoryProductServices = abstractInventoryProductServices;
        }
        #endregion

        //InventoryProduct_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("InventoryProduct_ById")]
        public async Task<IHttpActionResult> InventoryProduct_ById(long Id)
        {
            var quote = abstractInventoryProductServices.InventoryProduct_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //InventoryProduct_UpdateInventory Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("InventoryProduct_UpdateInventory")]
        public async Task<IHttpActionResult> InventoryProduct_UpdateInventory(InventoryProduct inventoryProduct)
        {
            var quote = abstractInventoryProductServices.InventoryProduct_UpdateInventory(inventoryProduct);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //InventoryProduct_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("InventoryProduct_Upsert")]
        public async Task<IHttpActionResult> InventoryProduct_Upsert()
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            InventoryProduct inventoryProduct = new InventoryProduct();
            var httpRequest = HttpContext.Current.Request;
            inventoryProduct.Id = Convert.ToInt64(httpRequest.Params["Id"]);
            inventoryProduct.SalonId = Convert.ToInt64(httpRequest.Params["SalonId"]);
            inventoryProduct.ProductName = Convert.ToString(httpRequest.Params["ProductName"]);
            inventoryProduct.ProductTypeId = Convert.ToInt64(httpRequest.Params["ProductTypeId"]);
            inventoryProduct.ProductBrandId = Convert.ToInt64(httpRequest.Params["ProductBrandId"]);
            inventoryProduct.VendorId = Convert.ToInt64(httpRequest.Params["VendorId"]);
            inventoryProduct.OfflineVendorId = Convert.ToInt64(httpRequest.Params["OfflineVendorId"]);
            inventoryProduct.BillNumber = Convert.ToString(httpRequest.Params["BillNumber"]);
            inventoryProduct.PurchaseDate = Convert.ToString(httpRequest.Params["PurchaseDate"]);
            inventoryProduct.ProductQty = Convert.ToInt64(httpRequest.Params["ProductQty"]);
            inventoryProduct.Price = Convert.ToDecimal(httpRequest.Params["Price"]);
            inventoryProduct.Weight = Convert.ToDecimal(httpRequest.Params["Weight"]);
            inventoryProduct.WeightTypeId = Convert.ToInt64(httpRequest.Params["WeightTypeId"]);
            inventoryProduct.LowQtyAlert = Convert.ToInt64(httpRequest.Params["LowQtyAlert"]);
            inventoryProduct.PurchaseVia = Convert.ToInt64(httpRequest.Params["PurchaseVia"]);
            inventoryProduct.CreatedBy = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);
            inventoryProduct.UpdatedBy = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);
            inventoryProduct.ExpiryDate = Convert.ToString(httpRequest.Params["ExpiryDate"]);
            inventoryProduct.IsExpiryDate = Convert.ToBoolean(httpRequest.Params["IsExpiryDate"]);

            if (httpRequest.Files.Count > 0)
            {
                var myFile = httpRequest.Files[0];
                string basePath = "InventoryProductImage/" + inventoryProduct.Id + "/";
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + Path.GetFileName(myFile.FileName);
                if (!Directory.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + basePath));
                }
                myFile.SaveAs(HttpContext.Current.Server.MapPath("~/" + basePath + fileName));
                inventoryProduct.ProductImage = basePath + fileName;
            }


            var quote = abstractInventoryProductServices.InventoryProduct_Upsert(inventoryProduct);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

       
        //InventoryProduct_UpdateQty Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("InventoryProduct_UpdateQty")]
        public async Task<IHttpActionResult> InventoryProduct_UpdateQty(InventoryProduct inventoryProduct)
        {
            var quote = abstractInventoryProductServices.InventoryProduct_UpdateQty(inventoryProduct);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //InventoryProduct_LowQtyAlert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("InventoryProduct_LowQtyAlert")]
        public async Task<IHttpActionResult> InventoryProduct_LowQtyAlert(InventoryProduct inventoryProduct)
        {
            var quote = abstractInventoryProductServices.InventoryProduct_LowQtyAlert(inventoryProduct);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }



        //InventoryProduct_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("InventoryProduct_All")]
        public async Task<IHttpActionResult> InventoryProduct_All(PageParam pageParam, string search=null,string ProductName=null, long ProductTypeId = 0, long ProductBrandId = 0, decimal Weight = 0, long WeightTypeId = 0, long SalonId = 0)
        {
            var quote = abstractInventoryProductServices.InventoryProduct_All(pageParam, search, ProductName, ProductTypeId, ProductBrandId, Weight, WeightTypeId, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //InventoryProduct_TopOne Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("InventoryProduct_TopOne")]
        public async Task<IHttpActionResult> InventoryProduct_TopOne(PageParam pageParam, string search = null, string ProductName = null,long ProductTypeId = 0,long ProductBrandId = 0,long SalonId = 0)
        {
            var quote = abstractInventoryProductServices.InventoryProduct_TopOne(pageParam,search, ProductName, ProductTypeId, ProductBrandId, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //InventoryProduct_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("InventoryProduct_Delete")]
        public async Task<IHttpActionResult> InventoryProduct_Delete(string Ids,long DeletedBy)
        {
            var quote = abstractInventoryProductServices.InventoryProduct_Delete(Ids, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

    }
}
