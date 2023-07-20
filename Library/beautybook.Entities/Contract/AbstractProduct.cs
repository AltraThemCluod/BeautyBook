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
    public abstract class AbstractProduct
    {
        public long Id { get; set; }
        public string ProductThumbnailImage { get; set; }
        public string VendorFirstname { get; set; }
        public string VendorSecondname { get; set; }
        public long VendorId { get; set; }
        public string ProductName { get; set; }
        public long ProductTypeId { get; set; }
        public long ProductBrandId { get; set; }
        public long ProductQty { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public long WeightTypeId { get; set; }
        public long LowQtyAlert { get; set; }
        public long LookUpStatusId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductBrandName { get; set; }
        public string ProductWeightTypeName { get; set; }
        public string LookUpStatusName { get; set; }
        public string ProductInformation { get; set; }
        public string ReturnPolicy { get; set; }
        public string HighlightsLabel { get; set; }
        public List<ProductHighlights> ProductHighlights { get; set; }
        public List<ProductServiceHighlights> ProductServiceHighlights { get; set; }
        public List<ProductSpecifications> ProductSpecifications { get; set; }
        public List<Users> ProductSeller { get; set; }
        public List<ProductImage> ProductImage { get; set; }
        public string HighlightsDiscription { get; set; }
        public string SpecificationsHighlightsLabel { get; set; }
        public string SpecificationsHighlightsDiscription { get; set; }
        public string ServiceHighlightsDiscription { get; set; }
        public string ProductImagesUrl { get; set; }
        public string ProductBrandIds { get; set; }
        public string ProductCategoryIds { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public long ProductCategoryId { get; set; }
        public long StarCount { get; set; }
        public long ReviewCount { get; set; }
        public long CustomersRecommend { get; set; }
        public Decimal ProductAverageRating { get; set; }
        public long ProductStarCount { get; set; }
        public long SortBy { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal OfferPercentage { get; set; }
        public decimal VendorAverageRating { get; set; }
        public long ProductTax { get; set; }
        public long ProductId { get; set; }
        public string ExpiryDate { get; set; }
        public bool IsExpiryDate { get; set; }

        public bool IsExpiryDateAfter30Day { get; set; }


        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

