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
using Newtonsoft.Json;
using beautybook.Models;
using System.Globalization;

namespace BeautyBookApi.Controllers.V1
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserAppointmentsV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractUserAppointmentsServices abstractUserAppointmentsServices;
        private object httpRequest;
        #endregion

        #region Cnstr
        public UserAppointmentsV1Controller(AbstractUserAppointmentsServices abstractUserAppointmentsServices)
        {
            this.abstractUserAppointmentsServices = abstractUserAppointmentsServices;
        }
        #endregion

        //UserAppointments_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserAppointments_ById")]
        public async Task<IHttpActionResult> UserAppointments_ById(long Id)
        {
            var quote = abstractUserAppointmentsServices.UserAppointments_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserAppointmentsInvoice_ById")]
        public async Task<IHttpActionResult> UserAppointmentsInvoice_ById(long Id)
        {
            var quote = abstractUserAppointmentsServices.UserAppointmentsInvoice_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("AppoinmentInvoice_ById")]
        public async Task<IHttpActionResult> AppoinmentInvoice_ById(long Id)
        {
            var quote = abstractUserAppointmentsServices.AppoinmentInvoice_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        [System.Web.Http.HttpPost]
        [InheritedRoute("GlobalAppoinmentInvoice_ById")]
        public async Task<IHttpActionResult> GlobalAppoinmentInvoice_ById(long Id)
        {
            var quote = abstractUserAppointmentsServices.AppoinmentInvoice_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //UserAppointments_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserAppointments_Upsert")]
        public async Task<IHttpActionResult> UserAppointments_Upsert(UserAppointments userAppointments)
        {
            var ServiceCategory = userAppointments.ServicesIds;
            userAppointments.ServicesIds = "";

            var quote = abstractUserAppointmentsServices.UserAppointments_Upsert(userAppointments);

            if (quote.Code == 200)
            {
                List<SD_Root> SD_Root = JsonConvert.DeserializeObject<List<SD_Root>>(ServiceCategory);
                
                foreach (var items in SD_Root)
                {
                    AppointmentServiceDetails ASDetails = new AppointmentServiceDetails();
                    ASDetails.Id = items.sd_serviceDetailsId;
                    ASDetails.CategoryId = items.sd_categoryName;
                    ASDetails.AppointmentId = quote.Item.Id;
                    ASDetails.ServiceId = items.sd_serviceName;
                    ASDetails.AssignedToUserId = items.sd_userAssignto;
                    ASDetails.Price = items.sd_appointmentsPrice;
                    ASDetails.Duration = items.sd_appointmentsDuration;

                    abstractUserAppointmentsServices.AppointmentServiceDetails_Upsert(ASDetails);
                }
            }

            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        //BlankAppointment_Create Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("BlankAppointment_Create")]
        public async Task<IHttpActionResult> BlankAppointment_Create(UserAppointments userAppointments)
        {
            var quote = abstractUserAppointmentsServices.BlankAppointment_Create(userAppointments);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //UserAppointments_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserAppointments_All")]
        public async Task<IHttpActionResult> UserAppointments_All(PageParam pageParam, string search, long SalonId, long CustomerId, long AssignedToUserId, string AppointmentDate, string AppointmentTime, long LookUpStatusId)
        {
            var quote = abstractUserAppointmentsServices.UserAppointments_All(pageParam, search, SalonId, CustomerId, AssignedToUserId, AppointmentDate, AppointmentTime, LookUpStatusId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //UserAppointments_GetAssignTo Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserAppointments_GetAssignTo")]
        public async Task<IHttpActionResult> UserAppointments_GetAssignTo(PageParam pageParam, string search, long SalonId =0, long CategoryId=0, string ServicesIds="" , string AppointmentDate="", string AppointmentTime = "", long UserAppointmentId=0)
        {

            //DateTime newDueDate = DateTime.ParseExact(AppointmentDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //string AppointmentDateConvert = AppointmentDate != null || AppointmentDate != "" ? newDueDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) : "";

            var quote = abstractUserAppointmentsServices.UserAppointments_GetAssignTo(pageParam, search, SalonId, CategoryId , ServicesIds , AppointmentDate, AppointmentTime, UserAppointmentId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //UserAppointments_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserAppointments_ChangeStatus")]
        public async Task<IHttpActionResult> UserAppointments_ChangeStatus(long Id,long LookUpStatusId,long LookUpStatusChangedBy)
        {
            string getQrCodeURL = "";
            if (LookUpStatusId == 10)
            {
                var AppointmentResponse = abstractUserAppointmentsServices.UserAppointments_ById(Id);

                getQrCodeURL = GetQrCode(
                    AppointmentResponse.Item.SalonName,
                    AppointmentResponse.Item.SalonCommercialRegisterNo,
                    ConvertTo.Double(AppointmentResponse.Item.ServicesTotalPrice),
                    ConvertTo.Double(AppointmentResponse.Item.Tax)
                );
            }

            var quote = abstractUserAppointmentsServices.UserAppointments_ChangeStatus(Id,LookUpStatusId, LookUpStatusChangedBy , getQrCodeURL + ".png");
            
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        public string GetQrCode(string SellerName,string TaxNo, double TotalAmt, double TaxAmt)
        {
            DateTime DtVal = DateTime.UtcNow;
            string GenFile = GenerateQrCode(SellerName, TaxNo, DtVal, TotalAmt, TaxAmt);

            return GenFile;
        }

        public string GenerateQrCode(string SellerName, string TaxNo, DateTime DtTime, Double Total, Double Tax)
        {
            TLVCls tlv = new TLVCls(SellerName, TaxNo, DtTime, Total, Tax);
            System.Drawing.Image img = tlv.toQrCode();

            string fileName = "";

            if (img != null)
            {
                string basePath = "SimplifiedInvoice/";
                fileName = DateTime.UtcNow.ToString("yyyyMMddhhmmssfff") + "_" + "QRCode";
                if (!Directory.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + basePath));
                }
                img.Save(HttpContext.Current.Server.MapPath("~/" + basePath + fileName + ".png"));
            }

            return fileName;
        }

        //UserAppointments_Delete Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserAppointments_Delete")]
        public async Task<IHttpActionResult> UserAppointments_Delete(long Id,long DeletedBy)
        {
            var quote = abstractUserAppointmentsServices.UserAppointments_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //AppointmentServiceDetails_Delete Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("AppointmentServiceDetails_Delete")]
        public async Task<IHttpActionResult> AppointmentServiceDetails_Delete(long Id)
        {
            var quote = abstractUserAppointmentsServices.AppointmentServiceDetails_Delete(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //SalonInvoice_CustomersAll Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonInvoice_CustomersAll")]
        public async Task<IHttpActionResult> SalonInvoice_CustomersAll(PageParam pageParam, string search = "", long SalonId = 0)
        {
            var quote = abstractUserAppointmentsServices.SalonInvoice_CustomersAll(pageParam, search, SalonId);
            return this.Content((HttpStatusCode)200, quote);
        }

        //SalonInvoice_All Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("SalonInvoice_All")]
        public async Task<IHttpActionResult> SalonInvoice_All(PageParam pageParam, string search = "", long SalonId = 0, string InvoiceDate = "", string InvoiceNumber = "", long CustomerId = 0)
        {
            var quote = abstractUserAppointmentsServices.SalonInvoice_All(pageParam, search, SalonId, InvoiceDate, InvoiceNumber, CustomerId);
            return this.Content((HttpStatusCode)200, quote);
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserReturnInvoice_ById")]
        public async Task<IHttpActionResult> UserReturnInvoice_ById(long Id)
        {
            var quote = abstractUserAppointmentsServices.UserReturnInvoice_ById(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //UserReturnInvoice_Update Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("UserReturnInvoice_Update")]
        public async Task<IHttpActionResult> UserReturnInvoice_Update(UserReturnInvoiceUpdate userReturnInvoiceUpdate)
        {
            var quote = abstractUserAppointmentsServices.UserReturnInvoice_Update(userReturnInvoiceUpdate);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //AppointmentServiceDetails_Upsert Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("AppointmentServiceDetails_Upsert")]
        public async Task<IHttpActionResult> AppointmentServiceDetails_Upsert(AppointmentServiceDetails appointmentServiceDetails)
        {
            var quote = abstractUserAppointmentsServices.AppointmentServiceDetails_Upsert(appointmentServiceDetails);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }
    }
}
