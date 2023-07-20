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
    public abstract class AbstractServicePackage
    {
        public long Id { get; set; }
        public long SalonId { get; set; }
        public string PackageName { get; set; }
        public decimal CustomPrice { get; set; }
        public string ServicePackageIdStr { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public List<MasterServicePackage> MasterServicePackage { get; set; }
        public string TotalPrice { get; set; }
        public string TotalDuration { get; set; }
        public decimal DailyDealsOfferPrice { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

