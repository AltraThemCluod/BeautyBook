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
    public abstract class AbstractOrderProducts
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime PurchasingDate { get; set; }
        public long VendorId { get; set; }
        public string VendorIdStr { get; set; } 
        public long AddToCartIds { get; set; }
        public string AddToCartIdStr { get; set; }
        public long SalonId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductRate { get; set; }
        public decimal ProductTax { get; set; }
        public long Qty { get; set; }
        public decimal ProductAmount { get; set; }
        public decimal ProductSubTotal { get; set; }
        public decimal ProductTaxAmount { get; set; }
        public decimal ProductTotal { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ProductWeight { get; set; }
        public string WeightTypeName { get; set; }
        public string ProductImage { get; set; }
       
        public decimal InvoiceProductDiscount { get; set; }
        public decimal InvoiceProductSubTotalWithDiscount { get; set; }
        public decimal InvoiceProductTaxAmount { get; set; }
        public decimal InvoiceProductTotal { get; set; }

        public decimal InvoiceUnitePrice { get; set; }
        public decimal InvoiceTaxPrice { get; set; }
        public decimal InvoiceProductRate { get; set; }
        public decimal InvoiceProductAmount { get; set; }
        public decimal InvoiceProductSubTotal { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        public string PurchasingDateStr => PurchasingDate != null ? PurchasingDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";

    }

    public class AbstractSalonOwnerOrdersCount
    {
        public long Id { get; set; }
        public long VendorId { get; set; }
        public long OrderId { get; set; }
        public bool IsView { get; set; }
    }

    public class AbstractTotalNewOrder
    {
        public long VendorId { get; set; }
        public long TotalNewOrder { get; set; }
    }
}

