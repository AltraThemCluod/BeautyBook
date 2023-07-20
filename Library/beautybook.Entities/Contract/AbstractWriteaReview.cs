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
    public abstract class AbstractWriteaReview
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public long RatingNo { get; set; }
        public string Heading { get; set; }
        public string Review { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string FirtsName { get; set; }
        public string SecondName { get; set; }
        public long VendorId { get; set; }
        public long VendorProductReviewCount { get; set; }
        public string VendorProductTotalRating { get; set; }
        public long CustomersRecommend { get; set; }

        public string VendorFirstname { get; set; }
        public string VendorSecondName { get; set; }
        public string VendorProfileUrl { get; set; }
        public string VendorPrimaryPhone { get; set; }
        public Decimal AverageRating { get; set; }
        public DateTime VendorCreatedDate { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";
        public string VendorCreatedDateStr => VendorCreatedDate != null ? VendorCreatedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";
    }
}
