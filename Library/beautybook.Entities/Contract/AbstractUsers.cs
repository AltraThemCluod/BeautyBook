using BeautyBook;
using BeautyBook.Entities.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BeautyBook.Entities.Contract
{
    public abstract class AbstractUsers
    {
        public long Id { get; set; }
        public long LookUpUserTypeId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string UserName { get; set; }
        public string EmployeeName { get; set; }
        public List<UserWorkingHours> UserWorkingHours { get; set; }
        public List<UserServices> UserServices { get; set; }
        public List<UserAppointments> UserAppointments { get; set; }
        public List<UserAppointments> AssignToUserAppointments { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsResetPasswordHash { get; set; }
        public bool IsEmailVerified { get; set; }
        public string EmailVerificationCode { get; set; }
        public DateTime EmailVerificationDate { get; set; }
        public string PrimaryPhone { get; set; }
        public bool IsPrimaryPhoneVerified { get; set; }
        public string PrimaryPhoneVerificationCode { get; set; }
        public string AlternatePhone { get; set; }
        public string JoiningDate { get; set; }
        public string ReferedByEmail { get; set; }
        public long LookUpEmployeeRolesId { get; set; }
        public string LookUpEmployeeRolesName { get; set; }
        public long LookUpEmployeeTypeId { get; set; }
        public string LookUpEmployeeTypeName { get; set; }

        public string UserRoleName { get; set; }
        public string UserTypeName { get; set; }


        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ResetPasswordCode { get; set; }
        public string ProfileUrl { get; set; }
        public string Tags { get; set; }
        public string Dob { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public long LookUpCountryId { get; set; }
        public long LookUpStateId { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public bool IsOnline { get; set; }
        public long LookUpStatusId { get; set; }
        public DateTime LookUpStatusChangedDate { get; set; }
        public long LookUpStatusChangedBy { get; set; }
        public string DeviceToken { get; set; }
        public string LastLogin { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public string WorkingStartTime { get; set; }
        public string WorkingEndTime { get; set; }
        public string WorkingHoursId { get; set; }
        public string UserWorkingServices { get; set; }
        public string UserWorkingDuration { get; set; }
        public string UserWorkingPrice { get; set; }
        public string UserWorkingServicesId { get; set; }
        public string LookUpStatusName { get; set; }
        public string LookUpUserTypeName { get; set; }
        public string LookUpCountry { get; set; }
        public string LookUpState { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get;set; }
        public string ConfirmPassword { get; set; }
        public long Type { get; set; }
        public long SalonId { get; set; }
        public string ReferedByUsername { get; set; }
        public string IsAvailable { get; set; }
        public string Name { get; set; }
        public long LoginType { get; set; }
        public string AppointmentsCount { get; set; }
        public string TotalSales { get; set; }
        public long TotalSalons { get; set; }
        public long ProductPrice { get; set; }
        public DateTime? UserAppoinmentLastVisit { get; set; }
        public decimal VendorAverageRating { get; set; }
        public long CustomerRecommend { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal OfferPercentage { get; set; }
        public long ProductId { get; set; }
        public long MasterIdenityId { get; set; }
        public long IsSeller { get; set; }
        public long NoOfEmployee { get; set; }
        public string MessageVendor { get; set; }
        public bool IsAdminApproved { get; set; }
        public long IsService { get; set; }
        public string BankName { get; set; }
        public string IBANNumber { get; set; }
        public string VATNumber { get; set; }
        public string IsLanguage { get; set; }
        public string CommercialRegisterNumber { get; set; }
        public string VendorCompanyNumber { get; set; }
        public bool IsEmployeeSendEmail { get; set; }
        public string EmployeePermissionData { get; set; }
        public string SalonName { get; set; }
        public string SalonLogoUrl { get; set; }

        [NotMapped]
        public string UserAppoinmentLastVisitStr => UserAppoinmentLastVisit != null ? UserAppoinmentLastVisit?.ToString("dd-MMM-yyyy") : "-";
        [NotMapped]
        public string CreatedVisitDate => CreatedDate != null ? CreatedDate?.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate?.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate?.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate?.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

