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
    public class UsersV1Controller : AbstractBaseController
    {
        #region Fields
        private readonly AbstractUsersServices abstractUsersServices;
        #endregion

        #region Cnstr
        public UsersV1Controller(AbstractUsersServices abstractUsersServices)
        {
            this.abstractUsersServices = abstractUsersServices;
        }
        #endregion

        //Users_ActInact Api
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [InheritedRoute("Users_ActInact")]
        public async Task<IHttpActionResult> Users_ActInact(long Id, long LookUpStatusId, long LookUpStatusChangedBy)
        {
            var quote = abstractUsersServices.Users_ActInact(Id, LookUpStatusId, LookUpStatusChangedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Users_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_ById")]
        public async Task<IHttpActionResult> Users_ById(long Id,string IsLanguage = "")
        {
            var quote = abstractUsersServices.Users_ById(Id, IsLanguage);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Employee_IsCreate Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Employee_IsCreate")]
        public async Task<IHttpActionResult> Employee_IsCreate(long SalonId)
        {
            var quote = abstractUsersServices.Employee_IsCreate(SalonId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //GetUser_ById Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("GetUser_ById")]
        public async Task<IHttpActionResult> GetUser_ById()
        {

            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            long Id = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);


            var quote = abstractUsersServices.Users_ById(Id , "en");
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        // Users_Login API
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_Login")]
        public async Task<IHttpActionResult> Users_Login(string Email, string PasswordHash, string DeviceToken, long LoginType , long LookUpUserTypeId)
        {
            var quote = abstractUsersServices.Users_Login(Email, PasswordHash, DeviceToken, LoginType, LookUpUserTypeId);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Users_MobileLogout API

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_MobileLogout")]
        public async Task<IHttpActionResult> Users_MobileLogout()
        {

            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            long Id = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractUsersServices.Users_MobileLogout(Id);
            return this.Content((HttpStatusCode)200, quote);
        }

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_Logout")]
        public async Task<IHttpActionResult> Users_Logout()
        {

            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            long Id = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractUsersServices.Users_Logout(Id);
            return this.Content((HttpStatusCode)200, quote);
        }

        //Users_ResetPassword API
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_ResetPassword")]
        public async Task<IHttpActionResult> Users_ResetPassword(long Id, string OldPassword, string NewPassword, string ConfirmPassword, long Type)
        {
            var quote = abstractUsersServices.Users_ResetPassword(Id, OldPassword, NewPassword, ConfirmPassword, Type);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        //Users_SignUp Api
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_SignUp")]
        public async Task<IHttpActionResult> Users_SignUp(Users users)
        {
            var quote = abstractUsersServices.Users_Upsert(users);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Users_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_Upsert")]
        public async Task<IHttpActionResult> Users_Upsert()
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            Users users = new Users();
            var httpRequest = HttpContext.Current.Request;
            users.Id = Convert.ToInt64(httpRequest.Params["Id"]);
            users.SalonId = Convert.ToInt64(httpRequest.Params["SalonId"]);
            users.LookUpUserTypeId = Convert.ToInt64(httpRequest.Params["LookUpUserTypeId"]);
            users.FirstName = Convert.ToString(httpRequest.Params["FirstName"]);
            users.SecondName = Convert.ToString(httpRequest.Params["SecondName"]);
            users.Gender = Convert.ToString(httpRequest.Params["Gender"]);
            users.Email = Convert.ToString(httpRequest.Params["Email"]);
            users.PasswordHash = Convert.ToString(httpRequest.Params["PasswordHash"]);
            users.PrimaryPhone = Convert.ToString(httpRequest.Params["PrimaryPhone"]);
            users.AlternatePhone = Convert.ToString(httpRequest.Params["AlternatePhone"]);            
            users.Tags = Convert.ToString(httpRequest.Params["Tags"]);
            users.Dob = Convert.ToString(httpRequest.Params["Dob"]);
             users.AddressLine1 = Convert.ToString(httpRequest.Params["AddressLine1"]);
            users.Latitude = Convert.ToString(httpRequest.Params["Latitude"]);
            users.Longitude = Convert.ToString(httpRequest.Params["Longitude"]);
            users.AddressLine2 = Convert.ToString(httpRequest.Params["AddressLine2"]);
            users.LookUpCountryId = Convert.ToInt64(httpRequest.Params["LookUpCountryId"]);
            users.LookUpStateId = Convert.ToInt64(httpRequest.Params["LookUpStateId"]);
            users.JoiningDate = Convert.ToString(httpRequest.Params["JoiningDate"]);
            users.LookUpEmployeeRolesId = Convert.ToInt64(httpRequest.Params["LookUpEmployeeRolesId"]);
            users.LookUpEmployeeTypeId = Convert.ToInt64(httpRequest.Params["LookUpEmployeeTypeId"]);
            users.City = Convert.ToString(httpRequest.Params["City"]);
            users.ZipCode = Convert.ToString(httpRequest.Params["ZipCode"]);
            users.CreatedBy = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);
            users.UpdatedBy = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);
            users.ReferedByEmail = Convert.ToString(httpRequest.Params["ReferedByEmail"]);
            users.ReferedByUsername = Convert.ToString(httpRequest.Params["ReferedByUsername"]);
            users.WorkingStartTime = Convert.ToString(httpRequest.Params["WorkingStartTime"]);
            users.WorkingEndTime = Convert.ToString(httpRequest.Params["WorkingEndTime"]);
            users.WorkingHoursId = Convert.ToString(httpRequest.Params["WorkingHoursId"]);
            users.IsAvailable = Convert.ToString(httpRequest.Params["IsAvailable"]);
            users.BankName = Convert.ToString(httpRequest.Params["BankName"]);
            users.IBANNumber = Convert.ToString(httpRequest.Params["IBANNumber"]);
            users.VATNumber = Convert.ToString(httpRequest.Params["VATNumber"]);
            users.CommercialRegisterNumber = Convert.ToString(httpRequest.Params["CommercialRegisterNumber"]);
            users.VendorCompanyNumber = Convert.ToString(httpRequest.Params["VendorCompanyNumber"]);

            if (httpRequest.Files.Count > 0)
            {
                var myFile = httpRequest.Files[0];
                string basePath = "UsersProfile/" + users.Id + "/";
                string fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + "_" + Path.GetFileName(myFile.FileName);
                if (!Directory.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/" + basePath))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + basePath));
                }
                myFile.SaveAs(HttpContext.Current.Server.MapPath("~/" + basePath + fileName));
                users.ProfileUrl = basePath + fileName;
            }

            var quote = abstractUsersServices.Users_Upsert(users);

            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        //BlankUsers_Upsert Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("BlankUsers_Create")]
        public async Task<IHttpActionResult> BlankUsers_Create()
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            Users users = new Users();
            var httpRequest = HttpContext.Current.Request;
            users.Id = Convert.ToInt64(httpRequest.Params["Id"]);
            users.SalonId = Convert.ToInt64(httpRequest.Params["SalonId"]);
            users.LookUpUserTypeId = Convert.ToInt64(httpRequest.Params["LookUpUserTypeId"]);
            users.CreatedBy = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);

            var quote = abstractUsersServices.BlankUsers_Create(users);

            return this.Content((HttpStatusCode)quote.Code, quote);
        }


        //Users_VerifyEmail Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_VerifyEmail")]
        public async Task<IHttpActionResult> Users_VerifyEmail(string EmailVerificationCode)
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            long Id = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);
            var quote = abstractUsersServices.Users_VerifyEmail(Id, EmailVerificationCode);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Users_VerifyEmail Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_VerifyCode")]
        public async Task<IHttpActionResult> Users_VerifyCode()
        {
            var claimsIdentity = (ClaimsIdentity)this.RequestContext.Principal.Identity;

            long Id = Convert.ToInt64(claimsIdentity.FindFirst("UserID").Value);
            var quote = abstractUsersServices.Users_VerifyCode(Id);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //Users_All Api

        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_All")]
        public async Task<IHttpActionResult> Users_All(PageParam pageParam, string search = "", long LookUpStatusId = 0,long LookUpUserTypeId = 0, string Name = "", long LookUpEmployeeTypeId = 0,long LookUpEmployeeRolesId = 0, string PrimaryPhone  = null, string Gender = null, long SalonId = 0,string IsLanguage = "")
        {

            AbstractUsers Users = new Users();
            Users.LookUpStatusId = LookUpStatusId;
            Users.LookUpUserTypeId = LookUpUserTypeId;
            Users.Name = Name;
            Users.LookUpEmployeeTypeId = LookUpEmployeeTypeId;
            Users.LookUpEmployeeRolesId = LookUpEmployeeRolesId;
            Users.PrimaryPhone = PrimaryPhone;
            Users.Gender = Gender;
            Users.SalonId = SalonId;
            Users.IsLanguage = IsLanguage;
            var quote = abstractUsersServices.Users_All(pageParam, search, Users);
            return this.Content((HttpStatusCode)200, quote);
        }

        //Users_VerifyEmail Api
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("Users_Delete")]
        public async Task<IHttpActionResult> Users_Delete(long Id,long DeletedBy)
        {
            var quote = abstractUsersServices.Users_Delete(Id, DeletedBy);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

        //User_ByEmail Api
        //[System.Web.Http.Authorize]
        [System.Web.Http.HttpPost]
        [InheritedRoute("User_ByEmail")]
        public async Task<IHttpActionResult> User_ByEmail(string Email , long IsSeller)
        {
            var quote = abstractUsersServices.User_ByEmail(Email, IsSeller);
            return this.Content((HttpStatusCode)quote.Code, quote);
        }

    }
}
