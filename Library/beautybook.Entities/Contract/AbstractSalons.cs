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
    public abstract class AbstractSalons
    {
        public long Id { get; set; }
        public long IsEmployee { get; set; }
        public long UserId { get; set; }
        public string SalonName { get; set; }
        public string LogoUrl { get; set; }
        public string PrimaryPhone { get; set; }
        public bool IsPrimaryPhoneVerified { get; set; }
        public string AlternatePhone { get; set; }
        public string AddressLine1 { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AddressLine2 { get; set; }
        public long LookUpCountryId { get; set; }
        public long LookUpStateId { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public long LookUpStatusId { get; set; }
        public DateTime LookUpStatusChangedDate { get; set; }
        public long LookUpStatusChangedBy { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public string BankName { get; set; }
        public string IBANNumber { get; set; }
        public string TaxNumber { get; set; }
        public string VATNumber { get; set; }
        public string CustomerSince { get; set; }
        public string TotalOrders { get; set; }
        public string LastOrder { get; set; }
        public string UserEmail { get; set; }
        public string UserPrimaryPhone { get; set; }
        public string UserAlternatePhone { get; set; }
        public List<OrderProducts> OrderProducts { get; set; }
        public long TotalSales { get; set; }
        public long NoOfBrand { get; set; }
        public long NoOfSales { get; set; }
        public string SalonProductBrand { get; set; }


        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

