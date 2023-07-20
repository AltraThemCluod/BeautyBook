using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BeautyBookApi.Models
{
    public class PaymentRequestModel
    {
        public string tran_ref { get; set; }
        public string tran_type { get; set; }
        public string cart_id { get; set; }
        public string cart_description { get; set; }
        public string cart_currency { get; set; }
        public decimal cart_amount { get; set; }
        public string callback { get; set; }
        public string redirect_url { get; set; }
        public int serviceId { get; set; }
        public string trace { get; set; }
    }

    
    //public class Result<T> where T : class
    //{
    //    public int Code { get; set; }

    //    public SubResult<T> Items { get; set; }

    //    public string Message { get; set; }
    //}

    //public class SubResult<T> where T : class
    //{
    //    public T Data { get; set; }
    //}

    //public class Users
    //{
    //    public string MobileNo { get; set; }
    //    public string Otp { get; set; }
    //    public string AggregatorReferalCode { get; set; }
    //    public string AadhaarCard { get; set; }
    //}

    //public class Aggregators
    //{
    //    public int    Id { get; set; }
    //    public string CompanyName { get; set; }
    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    public string LastName { get; set; }
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //    public string Salt { get; set; }
    //    public string Mobile { get; set; }
    //    public int    Gender { get; set; }
    //    public string Dob { get; set; }
    //    public int    StateId { get; set; }
    //    public int    DistrictId { get; set; }
    //    public int    TalukaId { get; set; }
    //    public string Village { get; set; }
    //    public string PANPhotoCopy { get; set; }
    //    public string PAN { get; set; }
    //    public int    IsPANVerified { get; set; }
    //    public string AadharPhotoCopy { get; set; }
    //    public string Aadhar { get; set; }
    //    public int    IsAadharVerified { get; set; }
    //    public string RationCard { get; set; }
    //    public string RationPhotoCopy { get; set; }
    //    public string GST { get; set; }
    //    public int    DefaultBankId { get; set; }
    //    public string Status { get; set; }
    //    public string Address { get; set; }
    //    public string DemantDraft { get; set; }
    //    public string Amount { get; set; }
    //    public string Commission { get; set; }
    //    public string CommissionReceived { get; set; }
    //    public string PendingCollection { get; set; }
    //    public string Deposit { get; set; }
    //    public string AvailableCreditLimit { get; set; }
    //    public int    StepsRemaining { get; set; }
    //    public string LastLogin { get; set; }
    //    public int    CreatedBy { get; set; }
    //    public string CreatedDate { get; set; }
    //    public int    ModifiedBy { get; set; }
    //    public string ModifiedDate { get; set; }
    //    public int    IsDelete { get; set; }
    //    public int    DeletedBy { get; set; }
    //    public string DeletedDate { get; set; }
    //    public string Otp { get; set; }
    //}

    //public class Customers
    //{
    //    public int    Id { get; set; }
    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    public string LastName { get; set; }
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //    public string Salt { get; set; }
    //    public string Mobile { get; set; }
    //    public int    Gender { get; set; }
    //    public string Dob { get; set; }
    //    public string AvtarPhotoCopy { get; set; }
    //    public string FarmerCardNumber { get; set; }
    //    public string LandArea { get; set; }
    //    public string PAN { get; set; }
    //    public int    StateId { get; set; }
    //    public int    DistrictId { get; set; }
    //    public int    TalukaId { get; set; }
    //    public string Village { get; set; }
    //    public string Latitude { get; set; }
    //    public string Longitude { get; set; }
    //    public string Aadhar { get; set; }
    //    public int    IsAadharVerified { get; set; }
    //    public int    AggregatorId { get; set; }
    //    public int    StepsRemaining { get; set; }
    //    public string Status { get; set; }
    //    public string LastLogin { get; set; }
    //    public int    CreatedBy { get; set; }
    //    public string CreatedDate { get; set; }
    //    public int    ModifiedBy { get; set; }
    //    public string ModifiedDate { get; set; }
    //    public int    DeletedBy { get; set; }
    //    public string DeletedDate { get; set; }
    //    public string Otp { get; set; }
    //}


}