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
    public abstract class AbstractOffer
    {
        public long Id { get; set; }
        public long SalonId { get; set; }
        public string OfferName { get; set; }
        public string ApplyOn { get; set; }
        public string OfferPeriodStart { get; set; }
        public string OfferPeriodEnd { get; set; }
        public string LookUpServicesIdStr { get; set; }
        public string OfferPriceStr { get; set; }
        public long AgeBetweenMinYear { get; set; }
        public long AgeBetweenMaxYear { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public string FullName { get; set; }
        public string PackagesIds { get; set; }
        public string PackagesOfferPrices { get; set; }
        public long PackagesId { get; set; }
        public decimal PackagesOfferPrice { get; set; }
        public bool IsType { get; set; }
        public List<MasterOfferPrice> MasterOfferPrice { get; set; }
        public List<PackageOfferPrice> PackageOfferPrice { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";

    }
}
