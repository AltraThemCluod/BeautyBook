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
    public abstract class AbstractDailyDeals
    {
        public long Id { get; set; }
        public long SalonId { get; set; }
        public string OfferDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public long ServiceAndPackagesId { get; set; }
        public long Type { get; set; }
        public decimal OfferPrice { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
        public string PackagesName { get; set; }
        public decimal PackagesPrice { get; set; }
        public string ServicesIds { get; set; }
        public string PackagesIds { get; set; }
        public string ServiceOfferPrice { get; set; }
        public string PackagesOfferPrice { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";       
    }
}

