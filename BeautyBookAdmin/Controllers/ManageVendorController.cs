using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;
using BeautyBookAdmin.Infrastructure;
using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyBookAdmin.Controllers
{
    public class ManageVendorController : BaseController
    {

        private readonly AbstractVendorApprovelServices abstractVendorApprovelServices = null;
        private readonly AbstractSalonsServices abstractSalonsServices = null;
        private readonly AbstractOrderProductsServices abstractOrderProductsServices = null;
        public ManageVendorController(
               AbstractVendorApprovelServices abstractVendorApprovelServices,
               AbstractSalonsServices abstractSalonsServices,
               AbstractOrderProductsServices abstractOrderProductsServices
           )
        {
            this.abstractVendorApprovelServices = abstractVendorApprovelServices;
            this.abstractSalonsServices = abstractSalonsServices;
            this.abstractOrderProductsServices = abstractOrderProductsServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        //MasterProductWeight all data get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ManageVendorData([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel , int IsApprove = 2)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractVendorApprovelServices.VendorApprovel_All(pageParam, search , IsApprove ,5);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //MasterProductType status actinact
        [HttpPost]
        public JsonResult ApprovedUnApprovedVendor(string ri = "MA==")
        {
            int Id = Convert.ToInt32(ConvertTo.Base64Decode(ri));
            SuccessResult<AbstractVendorApprovel> result = abstractVendorApprovelServices.VendorApprovel_IsApproved(Id);
            //result.Item = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        //GetSalonId
        [HttpPost]
        public JsonResult GetSalonData(string SalonDataId = "")
        {
            int Id = Convert.ToInt32(ConvertTo.Base64Decode(SalonDataId));

            var response = abstractSalonsServices.Salons_ById(Id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //GetGetVendorProduct
        [HttpPost]
        public JsonResult GetVendorProduct(string SalonDataId = "" , string VendorId = "")
        {
            int SId = Convert.ToInt32(ConvertTo.Base64Decode(SalonDataId));
            int VId = Convert.ToInt32(ConvertTo.Base64Decode(VendorId));

            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 999999;

            string search = "";

            var response = abstractOrderProductsServices.VendorProduct_ByVendorId(pageParam, search,VId,SId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


    }
}