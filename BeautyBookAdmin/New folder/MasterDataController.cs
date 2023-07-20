using DataTables.Mvc;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using BeautyBookAdmin.Infrastructure;
using BeautyBookAdmin.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BeautyBookAdmin.Controllers
{
    public class MasterDataController : BaseController
    {
        private readonly AbstractMasterProductTypeServices abstractMasterProductTypeServices = null;
        private readonly AbstractMasterProductBrandServices abstractMasterProductBrandServices = null;
        private readonly AbstractMasterProductWeightServices abstractMasterProductWeightServices = null;
        private readonly AbstractMasterOrderStatusServices abstractMasterOrderStatusServices = null;
        private readonly AbstractMasterProductCategoryServices abstractMasterProductCategoryServices= null;


        public MasterDataController(
                AbstractMasterProductTypeServices abstractMasterProductTypeServices,
                AbstractMasterProductBrandServices abstractMasterProductBrandServices,
                AbstractMasterProductWeightServices abstractMasterProductWeightServices,
                AbstractMasterOrderStatusServices abstractMasterOrderStatusServices,
                AbstractMasterProductCategoryServices abstractMasterProductCategoryServices
            )
        {
            this.abstractMasterProductTypeServices = abstractMasterProductTypeServices;
            this.abstractMasterProductBrandServices = abstractMasterProductBrandServices;
            this.abstractMasterProductWeightServices = abstractMasterProductWeightServices;
            this.abstractMasterOrderStatusServices = abstractMasterOrderStatusServices;
            this.abstractMasterProductCategoryServices = abstractMasterProductCategoryServices;

        }

        //MasterProductType Index action
        public ActionResult MasterProductType()
        {
            return View();
        }

        //MasterProductBrand Index action
        public ActionResult MasterProductBrand()
        {
            return View();
        }

        //MasterProductWeight Index action
        public ActionResult MasterProductWeight()
        {
            return View();
        }

        //MasterOrderStatus Index action
        public ActionResult MasterOrderStatus()
        {
            return View();
        }
        //MasterProductCategory Index action
        public ActionResult MasterProductCategory()
        {
            ViewBag.ProductType = MasterProductTypeDropdown();
            return View();
        }
        //********************************Master Product Type all method***************************************

        //MasterProductType all data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataMasterProductType([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractMasterProductTypeServices.MasterProductType_All(pageParam, search,true);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //MasterProductType status actinact
        [HttpPost]
        public JsonResult ActiveInActiveProductType(string ri = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            SuccessResult<AbstractMasterProductType> result = abstractMasterProductTypeServices.MasterProductType_ActInAct(Id);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Master Product type delete method
        [HttpPost]
        public JsonResult DeleteProductType(string ptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ptId));
            long DeletedBy = ProjectSession.AdminId;
            abstractMasterProductTypeServices.MasterProductType_Delete(Id, DeletedBy);
            return Json("Product Type Deleted successfully", JsonRequestBehavior.AllowGet);
        }

        //Master Product type insert
        [HttpPost]
        public JsonResult ProductTypeUpsert(HttpPostedFileBase uploadFile, long Id = 0, string Name = "")
        {
            MasterProductType model = new MasterProductType();
            model.Id = Id;
            model.Name = Name;
            model.CreatedBy = ProjectSession.AdminId;
            model.UpdatedBy = ProjectSession.AdminId;

            if (uploadFile != null)
            {
                string basePath = "ProductTypeImages/" + model.Id + "/";
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + Path.GetFileName(uploadFile.FileName);
                string path = Server.MapPath("~/" + basePath);
                if (!Directory.Exists(Path.Combine(HttpContext.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Server.MapPath("~/" + basePath));
                }
                uploadFile.SaveAs(HttpContext.Server.MapPath("~/" + basePath + fileName));
                model.ProductTypeImage = basePath + fileName;
            }


            var result = abstractMasterProductTypeServices.MasterProductType_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //getDate product type
        [HttpPost]
        public JsonResult GetDataProductTypeById(string  eptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractMasterProductType> successResult = abstractMasterProductTypeServices.MasterProductType_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }


        //********************************Master Product Brand all method***************************************

        //MasterProductBrand all data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataMasterProductBrand([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractMasterProductBrandServices.MasterProductBrand_All(pageParam, search,0);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //MasterProductBrand status actinact
        [HttpPost]
        public JsonResult ActiveInActiveProductBrand(string ri = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            SuccessResult<AbstractMasterProductBrand> result = abstractMasterProductBrandServices.MasterProductBrand_ActInAct(Id);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Master Product Brand delete method
        [HttpPost]
        public JsonResult DeleteProductBrand(string ptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ptId));
            long DeletedBy = ProjectSession.AdminId;
            abstractMasterProductBrandServices.MasterProductBrand_Delete(Id, DeletedBy);
            return Json("Product Brand Deleted successfully", JsonRequestBehavior.AllowGet);
        }

        //Master Product Brand insert
        [HttpPost]
        public JsonResult ProductBrandUpsert(long Id = 0, string Name = "")
        {
            MasterProductBrand model = new MasterProductBrand();
            model.Id = Id;
            model.Name = Name;
            model.CreatedBy = ProjectSession.AdminId;
            model.UpdatedBy = ProjectSession.AdminId;
            var result = abstractMasterProductBrandServices.MasterProductBrand_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //getDate product Brand
        [HttpPost]
        public JsonResult GetDataProductBrandById(string eptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractMasterProductBrand> successResult = abstractMasterProductBrandServices.MasterProductBrand_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }

        //********************************Master Product Weight all method***************************************

        //MasterProductWeight all data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataMasterProductWeight([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractMasterProductWeightServices.MasterProductWeight_All(pageParam, search);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //MasterProductWeight status actinact
        [HttpPost]
        public JsonResult ActiveInActiveProductWeight(string ri = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            SuccessResult<AbstractMasterProductWeight> result = abstractMasterProductWeightServices.MasterProductWeight_ActInAct(Id);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Master Product Weight delete method
        [HttpPost]
        public JsonResult DeleteProductWeight(string ptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ptId));
            long DeletedBy = ProjectSession.AdminId;
            abstractMasterProductWeightServices.MasterProductWeight_Delete(Id, DeletedBy);
            return Json("Product Weight Deleted successfully", JsonRequestBehavior.AllowGet);
        }

        //Master Product Brand insert
        [HttpPost]
        public JsonResult ProductWeightUpsert(long Id = 0, string Name = "")
        {
            MasterProductWeight model = new MasterProductWeight();
            model.Id = Id;
            model.Name = Name;
            model.CreatedBy = ProjectSession.AdminId;
            model.UpdatedBy = ProjectSession.AdminId;
            var result = abstractMasterProductWeightServices.MasterProductWeight_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //getDate product Weight
        [HttpPost]
        public JsonResult GetDataProductWeightById(string eptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractMasterProductWeight> successResult = abstractMasterProductWeightServices.MasterProductWeight_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }


        //********************************Master Order Ststua all method***************************************

        //MasterProductWeight all data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataMasterOrderStatus([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractMasterOrderStatusServices.MasterOrderStatus_All(pageParam, search);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //MasterOrderStatus actinact
        [HttpPost]
        public JsonResult ActiveInActiveOrderStatus(string ri = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            SuccessResult<AbstractMasterOrderStatus> result = abstractMasterOrderStatusServices.MasterOrderStatus_ActInAct(Id);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Master Order Status delete method
        [HttpPost]
        public JsonResult DeleteOrderStatus(string ptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ptId));
            long DeletedBy = ProjectSession.AdminId;
            abstractMasterOrderStatusServices.MasterOrderStatus_Delete(Id, DeletedBy);
            return Json("Order Status Deleted successfully", JsonRequestBehavior.AllowGet);
        }

        //Master Order Status insert
        [HttpPost]
        public JsonResult OrderStatusUpsert(long Id = 0, string Name = "")
        {
            MasterOrderStatus model = new MasterOrderStatus();
            model.Id = Id;
            model.Name = Name;
            model.CreatedBy = ProjectSession.AdminId;
            model.UpdatedBy = ProjectSession.AdminId;
            var result = abstractMasterOrderStatusServices.MasterOrderStatus_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //getDate Order Status
        [HttpPost]
        public JsonResult GetDataOrderStatusById(string eptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractMasterOrderStatus> successResult = abstractMasterOrderStatusServices.MasterOrderStatus_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }

        //MasterProductTypeDropdown drp data get
        public IList<SelectListItem> MasterProductTypeDropdown()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;

                var models = abstractMasterProductTypeServices.MasterProductType_All(pageParam, "",false);

                foreach (var master in models.Values)
                {
                    items.Add(new SelectListItem() { Text = master.Name.ToString(), Value = Convert.ToString(master.Id) });
                }


                return items;
            }
            catch (Exception)
            {
                return items;
            }
        }

        //ViewAllDataMasterProductCategory all data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataMasterProductCategory([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel,int ProductTypeId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractMasterProductCategoryServices.MasterProductCategory_All(pageParam, search, ProductTypeId);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }
        //MasterProductType status actinact
        [HttpPost]
        public JsonResult ActiveInActiveProductCategory(string ri = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            SuccessResult<AbstractMasterProductCategory> result = abstractMasterProductCategoryServices.MasterProductCategory_ActInact(Id);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Master Product type delete method
        [HttpPost]
        public JsonResult DeleteProductCategory(string ptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(ptId));
            long DeletedBy = ProjectSession.AdminId;
            abstractMasterProductCategoryServices.MasterProductCategory_Delete(Id, DeletedBy);
            return Json("Product Category Deleted successfully", JsonRequestBehavior.AllowGet);
        }

        //Master Product Category insert
        [HttpPost]
        public JsonResult ProductCategoryUpsert(long Id = 0, long ProductTypeId=0,string ProductCategoryName ="")
        {
            MasterProductCategory model = new MasterProductCategory();
            model.Id = Id;
            model.ProductTypeId = ProductTypeId;
            model.ProductCategoryName = ProductCategoryName;
            model.CreatedBy = ProjectSession.AdminId;
            model.UpdatedBy = ProjectSession.AdminId;

            var result = abstractMasterProductCategoryServices.MasterProductCategory_Upsert(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //getDate product Category
        [HttpPost]
        public JsonResult GetDataProductCategoryById(string eptId = "MA==")
        {
            long Id = Convert.ToInt32(ConvertTo.Base64Decode(eptId));
            SuccessResult<AbstractMasterProductCategory> successResult = abstractMasterProductCategoryServices.MasterProductCategory_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }
    }
}