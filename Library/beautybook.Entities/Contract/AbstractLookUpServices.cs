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
    public abstract class AbstractLookUpServices
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public string IconUrl { get; set; }
        public string ServPhotoUrl { get; set; }
        public long LookUpStatusId { get; set; }
        public DateTime LookUpStatusChangedDate { get; set; }
        public long LookUpStatusChangedBy { get; set; }
        public decimal Price { get; set; }
        public decimal Duration { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public long UserId { get; set; }
        public long SalonId { get; set; }
        public long PackageId { get; set; }
        public decimal PackageCustomPrices { get; set; }
        public long IsAllServices { get; set; }
        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

