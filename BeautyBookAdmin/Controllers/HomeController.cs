using BeautyBook.Common;
using BeautyBook.Common.Paging;
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
    public class HomeController : BaseController
    {

        private readonly AbstractDashboardDataServices abstractDashboardDataServices = null;
        private readonly AbstractLookUpCountryServices abstractLookUpCountryServices = null;
        private readonly AbstractLookUpStateServices abstractLookUpStateServices = null;
        private readonly AbstractOrdersServices abstractOrdersServices = null;

        public HomeController(AbstractDashboardDataServices abstractDashboardDataServices,
            AbstractLookUpCountryServices abstractLookUpCountryServices,
            AbstractLookUpStateServices abstractLookUpStateServices,
            AbstractOrdersServices abstractOrdersServices
            )

        {
            this.abstractDashboardDataServices = abstractDashboardDataServices;
            this.abstractLookUpCountryServices = abstractLookUpCountryServices;
            this.abstractLookUpStateServices = abstractLookUpStateServices;
            this.abstractOrdersServices = abstractOrdersServices;
        }

        public ActionResult Index()
        {
            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 10000;
            var response = abstractDashboardDataServices.DashboardData_All(pageParam, "",0,0);

            ViewBag.NumberOfSalonOwner = response.TotalSalonOwner;
            ViewBag.NumberOfSeller = response.TotalSeller;
            ViewBag.NumberOfSalon = response.TotalSalon;


            return View();
        }
        public ActionResult Sales(string SalonIdSales = "")
        {

            ViewBag.SalonIdInt = Convert.ToInt32(ConvertTo.Base64Decode(SalonIdSales));

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalonOwnerDataGet([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long LookUpCountry = 0, long LookUpState = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);

                var response = abstractDashboardDataServices.DashboardData_All(pageParam, search, LookUpCountry, LookUpState);

                totalRecord = 1;
                filteredRecord = 1;

                return Json(new DataTablesResponse(requestModel.Draw, response.UsersSalonOwner, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SellerDataGet([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long LookUpCountry = 0, long LookUpState = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);

                var response = abstractDashboardDataServices.DashboardData_All(pageParam, search, LookUpCountry, LookUpState);

                totalRecord = 1;
                filteredRecord = 1;

                return Json(new DataTablesResponse(requestModel.Draw, response.UsersSallers, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalonBranchDataGet([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long LookUpCountry = 0, long LookUpState = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);

                var response = abstractDashboardDataServices.DashboardData_All(pageParam, search, LookUpCountry, LookUpState);

                totalRecord = 1;
                filteredRecord = 1;

                return Json(new DataTablesResponse(requestModel.Draw, response.SalonBranch, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //getData Master Country
        [HttpPost]
        public JsonResult GetDataMasterCountry()
        {

            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 999999;
            var response = abstractLookUpCountryServices.LookUpCountry_All(pageParam, "");
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //getData Master State
        [HttpPost]
        public JsonResult GetDataMasterState(long CountryId = 0)
        {

            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 999999;
            var response = abstractLookUpStateServices.LookUpState_All(pageParam, "" , CountryId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //Sales page action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalonSalesDataGet([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel , int SalonId = 0 , string DateOfOrder = "")
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);

                var response = abstractOrdersServices.Orders_All(pageParam, search, 0,null, DateOfOrder, SalonId, 0);

                totalRecord = 1;
                filteredRecord = 1;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }
    }
}