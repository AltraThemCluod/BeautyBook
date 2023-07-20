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
    public abstract class AbstractAddToCart
    {
        public int Id { get; set; }
        public long SalonId { get; set; }
        public long VendorId { get; set; }
        public long ProductId { get; set; }
        public long Qty { get; set; }
        public string ProductIds { get; set; }
        public string Qtys { get; set; }
        public string ProductThumbnailImage { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductTotalAmount { get; set; }
        public decimal ProductSubTotal { get; set; }
        public decimal ProductTaxAmount { get; set; }
        public decimal ProductTotal { get; set; }
        public long ProductTax { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";

    }
}
