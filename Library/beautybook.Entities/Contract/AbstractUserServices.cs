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
    public abstract class AbstractUserServices
    {
        public long Id { get; set; }
        public string EmployeeFirstname { get; set; }
        public string EmployeeSecondname { get; set; }
        public string EmployeeProfileUrl { get; set; }
        public long UserId { get; set; }
        public long SalonServiceId { get; set; }
        public string LookUpServicesName { get; set; }
        public long LookUpServicesId { get; set; }
        public long LookUpCategoryId { get; set; }
        public string LookUpCategoryName { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }
        public long LookUpStatusId { get; set; }
        public long LookUpStatusSalonServiceId { get; set; }
        public string SalonServiceLookUpStatus { get; set; }
        public DateTime LookUpStatusChangedDate { get; set; }
        public long LookUpStatusChangedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

