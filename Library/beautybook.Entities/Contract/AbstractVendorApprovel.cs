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
    public abstract class AbstractVendorApprovel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Firstname { get; set; }
        public string SecondName { get; set; }
        public long LookUpUserTypeId { get; set; }
        public string Gender { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string PrimaryPhone { get; set; }
        public string AlternatePhone { get; set; }
        public string ProfileUrl { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public long LookUpCountryId { get; set; }
        public string CountryName { get; set; }
        public long LookUpStateId { get; set; }
        public string StateName { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Message { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }

        public string SalonIds { get; set; }
        public string salonNames { get; set; }
        public DateTime LastTransactionDate { get; set; }
        public long NoOfSales { get; set; }
        public decimal VendorTotalSales { get; set; }
        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string LastTransactionDateStr => LastTransactionDate != null ? LastTransactionDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

