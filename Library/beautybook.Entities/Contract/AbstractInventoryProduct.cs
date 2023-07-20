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
    public abstract class AbstractInventoryProduct
    {
        public long Id { get; set; }
        public string Ids { get; set; }
        public long InventoryOfflineId { get; set; }
        public long InventoryOnlineId { get; set; }
        public string InventoryProductIds { get; set; }
        public string ProductName { get; set; }
        public long ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public long ProductBrandId { get; set; }
        public string ProductBrandName { get; set; }
        public long SalonId { get; set; }
        public long VendorId { get; set; }
        public long OfflineVendorId { get; set; }
        public string VendorOfflineName { get; set; }
        public string VendorName { get; set; }
        public string BillNumber { get; set; }
        public string PurchaseDate { get; set; }
        public long ProductQty { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public long WeightTypeId { get; set; }
        public string ProductWeightTypeName { get; set; }
        public string ProductWeightName { get; set; }
        public long LowQtyAlert { get; set; }
        public long PurchaseVia { get; set; }
        public string ProductImage { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public string OrderNo { get; set; }
        public string PurchaseViaName { get; set; }
        public string Total { get; set; }
        public long SumProductQty { get; set; }
        public string ProductThumbnailImage { get; set; }
        public bool IsExpiryDate { get; set; }
        public string ExpiryDate { get; set; }
        public bool IsExpiryDateAfter30Day { get; set; }
        public long NoOfDay { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

