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
using System.Web;
using System.Web.Mvc;

namespace BeautyBookAdmin.Controllers
{
    public class SalonsDataController : BaseController
    {
        private readonly AbstractUsersServices abstractUsersServices = null;
        private readonly AbstractLookUpEmployeeTypeServices abstractLookUpEmployeeTypeServices = null;
        private readonly AbstractLookUpEmployeeRolesServices abstractLookUpEmployeeRolesServices = null;
        private readonly AbstractSalonServicesServices abstractSalonServicesServices = null;
        private readonly AbstractLookUpServicesServices abstractLookUpServicesServices = null;
        private readonly AbstractUserLeaveServices abstractUserLeaveServices = null;
        private readonly AbstractLookUpLeaveTypeServices abstractLookUpLeaveTypeServices= null;
        private readonly AbstractUserWorkSheetServices abstractUserWorkSheetServices = null;
        private readonly AbstractUserAppointmentsServices abstractUserAppointmentsServices = null;
        private readonly AbstractUserServicesServices abstractUserServicesServices = null;
        
        public SalonsDataController(
                AbstractUsersServices abstractUsersServices , 
                AbstractLookUpEmployeeTypeServices abstractLookUpEmployeeTypeServices,
                AbstractLookUpEmployeeRolesServices abstractLookUpEmployeeRolesServices,
                AbstractSalonServicesServices abstractSalonServicesServices,
                AbstractLookUpServicesServices abstractLookUpServicesServices,
                AbstractUserLeaveServices abstractUserLeaveServices,
                AbstractLookUpLeaveTypeServices abstractLookUpLeaveTypeServices,
                AbstractUserWorkSheetServices abstractUserWorkSheetServices,
                AbstractUserAppointmentsServices abstractUserAppointmentsServices,
                AbstractUserServicesServices abstractUserServicesServices
            )
        {
            this.abstractUsersServices = abstractUsersServices;
            this.abstractLookUpEmployeeTypeServices = abstractLookUpEmployeeTypeServices;
            this.abstractLookUpEmployeeRolesServices = abstractLookUpEmployeeRolesServices;
            this.abstractSalonServicesServices = abstractSalonServicesServices;
            this.abstractLookUpServicesServices = abstractLookUpServicesServices;
            this.abstractUserLeaveServices = abstractUserLeaveServices;
            this.abstractLookUpLeaveTypeServices = abstractLookUpLeaveTypeServices;
            this.abstractUserWorkSheetServices = abstractUserWorkSheetServices;
            this.abstractUserAppointmentsServices = abstractUserAppointmentsServices;
            this.abstractUserServicesServices = abstractUserServicesServices;
        }

        //SalonsData Index action
        public ActionResult ManageCustomers(string SalonId = "MjIy" , string SalonOwnerId = "MjIy")
        {
            ViewBag.salonId = Convert.ToInt32(ConvertTo.Base64Decode(SalonId));
            ViewBag.salonOwnerId = SalonOwnerId;
            return View();
        }

        //ManageEmployeesDate Index action
        public ActionResult ManageEmployees(string SalonId = "MjIy", string SalonOwnerId = "MjIy")
        {
            ViewBag.EmployeesSalonId = Convert.ToInt32(ConvertTo.Base64Decode(SalonId));
            ViewBag.salonOwnerId = SalonOwnerId;
            
            ViewBag.EmployeeType = EmployeeTypeDropdown();
            ViewBag.EmployeeRoles = EmployeeRolesDropdown();

            return View();
        }

        //ManageEmployeesLeave Index action
        public ActionResult ManageEmployeeLeaves(string SalonId = "MjIy", string SalonOwnerId = "MjIy")
        {
            ViewBag.EmployeesSalonId = Convert.ToInt32(ConvertTo.Base64Decode(SalonId));
            ViewBag.salonOwnerId = SalonOwnerId;
            
            ViewBag.EmployeeLeaveType = EmployeeLeaveTypeDropDown();
            ViewBag.EmployeeRoles = EmployeeRolesDropdown();
            ViewBag.EmployeeList = EmployeeListDropdown(ViewBag.EmployeesSalonId);

            return View();
        }

        //ManageEmployeeWorksheet Index action
        public ActionResult ManageEmployeeWorksheet(string SalonId = "MjIy", string SalonOwnerId = "MjIy")
        {
            ViewBag.EmployeesSalonId = Convert.ToInt32(ConvertTo.Base64Decode(SalonId));
            ViewBag.salonOwnerId = SalonOwnerId;

            ViewBag.EmployeeLeaveType = EmployeeLeaveTypeDropDown();
            ViewBag.EmployeeRoles = EmployeeRolesDropdown();
            ViewBag.EmployeeList = EmployeeListDropdown(ViewBag.EmployeesSalonId);

            return View();
        }

        //ManageSalonServices Index action
        public ActionResult ManageSalonServices(string SalonId = "MjIy" , string SalonOwnerId = "MjIy")
        {
            ViewBag.SalonServicesSalonId = Convert.ToInt32(ConvertTo.Base64Decode(SalonId));
            ViewBag.salonOwnerId = SalonOwnerId;

            ViewBag.SalonCategory = LookUpCategoryDropdown();
            //ViewBag.SalonService = LookUpServicesDropdown();

            return View();
        }

        //ManageUserAppointments Index action
        public ActionResult ManageAppointments(string SalonId = "MjIy", string SalonOwnerId = "MjIy")
        {
            ViewBag.UserAppointmentsSalonId = Convert.ToInt32(ConvertTo.Base64Decode(SalonId));
            ViewBag.salonOwnerId = SalonOwnerId;

            //ViewBag.SalonCategory = LookUpCategoryDropdown();
            //ViewBag.SalonService = LookUpServicesDropdown();

            ViewBag.CustomerList = CustomerListDropdown(ViewBag.UserAppointmentsSalonId);
            ViewBag.EmployeeList = EmployeeListDropdown(ViewBag.UserAppointmentsSalonId);
            return View();
        }

        //ManageSalonServices Index action
        public ActionResult ManageEmployeeAndCharges(string SalonId = "MjIy", string SalonOwnerId = "MjIy", string SalonServieId = "MjIy")
        {
            ViewBag.SalonServicesSalonId = Convert.ToInt32(ConvertTo.Base64Decode(SalonId));
            ViewBag.salonOwnerId = SalonOwnerId;
            ViewBag.SalonServicesId = Convert.ToInt32(ConvertTo.Base64Decode(SalonServieId));
            return View();
        }

        //EmployeeType drp data get
        public IList<SelectListItem> EmployeeTypeDropdown()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;

                var models = abstractLookUpEmployeeTypeServices.LookUpEmployeeType_All(pageParam, "");

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

        //EmployeeLeaveType drp data get
        public IList<SelectListItem> EmployeeLeaveTypeDropDown()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;

                var models = abstractLookUpLeaveTypeServices.LookUpLeaveType_All(pageParam, "");

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

        //EmployeeType drp data get
        public IList<SelectListItem> EmployeeRolesDropdown()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;

                var models = abstractLookUpEmployeeRolesServices.LookUpEmployeeRoles_All(pageParam, "","en");

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

        //Category drp data get

        public IList<SelectListItem> LookUpCategoryDropdown(long ParentId = 0)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;

                var models = abstractLookUpServicesServices.LookUpServices_All(pageParam, "", ParentId,0,0);

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

        //Services drp data get
        [HttpPost]
        public JsonResult BindServicesDropdown(int ParentId = 0)
        {
            PageParam pageParam = new PageParam();
            pageParam.Offset = 0;
            pageParam.Limit = 0;

            var result = abstractLookUpServicesServices.LookUpServices_All(pageParam, "", ParentId,0,0);
            return Json(result.Values, JsonRequestBehavior.AllowGet);
        }

        // Customer list
        public IList<SelectListItem> CustomerListDropdown(long salonId = 0)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;
                AbstractUsers users = new Users();
                users.LookUpUserTypeId = 4;
                users.SalonId = salonId;
                var models = abstractUsersServices.Users_All(pageParam, "", users);

                foreach (var master in models.Values)
                {
                    items.Add(new SelectListItem() { Text = master.UserName.ToString(), Value = Convert.ToString(master.Id) });
                }
                return items;
            }
            catch (Exception)
            {
                return items;
            }
        }

        // Employee list
        public IList<SelectListItem> EmployeeListDropdown(long salonId = 0)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;
                AbstractUsers users = new Users();
                users.LookUpUserTypeId = 3;
                users.SalonId = salonId;
                var models = abstractUsersServices.Users_All(pageParam, "", users);

                foreach (var master in models.Values)
                {
                    items.Add(new SelectListItem() { Text = master.UserName.ToString(), Value = Convert.ToString(master.Id) });
                }
                return items;
            }
            catch (Exception)
            {
                return items;
            }
        }

        //Salon all customer list get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataCustomer([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel , long SalonId = 0 , string Name = "", string PrimaryPhone = "" , string Gender = "")
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                Users model = new Users();
                model.SalonId = SalonId;
                model.LookUpUserTypeId = 4;
                model.Name = Name;
                model.PrimaryPhone = PrimaryPhone;
                model.Gender = Gender;
                var response = abstractUsersServices.Users_All(pageParam, search, model);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //Salon all Employees list get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataEmployees([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long SalonId = 0 , string Name = "" , int LookUpEmployeeTypeId = 0, int LookUpEmployeeRolesId = 0, int LookUpStatusId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                Users model = new Users();
                model.SalonId = SalonId;
                model.LookUpUserTypeId = 3;
                model.Name = Name;
                model.LookUpEmployeeTypeId = LookUpEmployeeTypeId;
                model.LookUpEmployeeRolesId = LookUpEmployeeRolesId;
                model.LookUpStatusId = LookUpStatusId;
                var response = abstractUsersServices.Users_All(pageParam, search, model);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //Salon all Employees Leaves list get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataEmployeeLeaves([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel,long UserId = 0, long SalonId = 0, long LookUpLeaveTypeId = 0, string FromDate = "", string ToDate = "", long LookUpStatusId = 0,long LookUpEmployeeRolesId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                UserLeave model = new UserLeave();
                model.UserId = UserId;
                model.SalonId = SalonId;
                model.LookUpLeaveTypeId = LookUpLeaveTypeId;
                model.FromDate = FromDate;
                model.ToDate = ToDate;
                model.LookUpStatusId = LookUpStatusId;
                model.LookUpEmployeeRolesId = LookUpEmployeeRolesId;
                var response = abstractUserLeaveServices.UserLeave_All(pageParam, search, model);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //Salon all Employees Work Sheet list get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataEmployeeWorkSheet([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long UserId = 0, long SalonId = 0, string AttendanceDate = "", long LookUpStatusId = 0, long LookUpEmployeeRolesId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                UserWorkSheet model = new UserWorkSheet();
                model.UserId = UserId;
                model.SalonId = SalonId;
                //var attendanceDateFormatted = Convert.ToDateTime(AttendanceDate).ToString("yyyy-MM-dd");
                model.AttendanceDate = AttendanceDate;
                model.LookUpStatusId = LookUpStatusId;
                model.LookUpEmployeeRolesId = LookUpEmployeeRolesId;
                var response = abstractUserWorkSheetServices.UserWorkSheet_All(pageParam, search, model);

                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }


        //SalonServices all list get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataSalonServices([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long SalonId = 0 , long LookUpServicesId = 0, long LookUpStatusId = 0, long LookUpCategoryId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractSalonServicesServices.SalonServices_All(pageParam, search, SalonId , LookUpServicesId , LookUpStatusId, LookUpCategoryId);
                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //UserAppointment all list get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataAppointments([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long SalonId = 0, long CustomerId = 0, long AssignedToUserId = 0, string AppointmentDate = "", string AppointmentTime = "", long LookUpStatusId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;

                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractUserAppointmentsServices.UserAppointments_All(pageParam, search, SalonId, CustomerId, AssignedToUserId, AppointmentDate, AppointmentTime, LookUpStatusId);
                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

        //Customer Details get
        [HttpPost]
        public JsonResult ViewDataCustomerDetails(long Id = 0)
        {
            var successResult = abstractUsersServices.Users_ById(Id , "");
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }
       
        //employee leave count all list get
        [HttpPost]
        public JsonResult ViewAllDataEmployeeLeavecount(long UserId = 0, long SalonId = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = 0;
                pageParam.Limit = 0;

                UserLeave model = new UserLeave();
                model.UserId = UserId;
                model.SalonId = SalonId;

                var response = abstractUserLeaveServices.UserLeave_LeaveTypeCount(pageParam, model);
                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }


        //Appointment data get
        [HttpPost]
        public JsonResult GetAppointmentDetaislById(long Id = 0)
        {
            SuccessResult<AbstractUserAppointments> successResult = abstractUserAppointmentsServices.UserAppointments_ById(Id);
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }


        //EmployeeAndCharges all list get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ViewAllDataEmployeeAndCharges([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, long SalonServicesById = 0)
        {
            {
                int totalRecord = 0;
                int filteredRecord = 0;

                PageParam pageParam = new PageParam();
                pageParam.Offset = requestModel.Start;
                pageParam.Limit = requestModel.Length;


                string search = Convert.ToString(requestModel.Search.Value);
                var response = abstractUserServicesServices.UserServices_SalonServicesById(pageParam, search, SalonServicesById);
                totalRecord = (int)response.TotalRecords;
                filteredRecord = (int)response.TotalRecords;

                return Json(new DataTablesResponse(requestModel.Draw, response.Values, filteredRecord, totalRecord), JsonRequestBehavior.AllowGet);
            }
        }

    }
}