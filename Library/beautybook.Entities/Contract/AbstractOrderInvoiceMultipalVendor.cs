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
    public abstract class AbstractOrderInvoiceMultipalVendor
    {
        public long Id { get; set; }
        public string Supplername { get; set; }
        public string VendorCompanyName { get; set; }
        public string CommercialRegisterNumber { get; set; }
        public string VATNumber { get; set; }
        public string SupplerAddress { get; set; }
        public string SupplerCountryName { get; set; }
        public string SupplerStateName { get; set; }
        public string SupplerCityName { get; set; }
        public string SupplerZipCode { get; set; }
        public string InvoicePrintDate { get; set; }  
        public string OrderDate { get; set; }
        public string InvoiceNo { get; set; }
        public List<OrderObject> OrderObject { get; set; }
        public string OrderObjectStr { get; set; }
        public List<SalonObject> SalonObject { get; set; }
        public string SalonObjectStr { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class OrderObject
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int VendorId { get; set; }
        public int AddToCartIds { get; set; }
        public int SalonId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductRate { get; set; }
        public int ProductTax { get; set; }
        public int Qty { get; set; }
        public int ProductAmount { get; set; }
        public int ProductSubTotal { get; set; }
        public decimal InvoiceProductDiscount { get; set; }
        public decimal ProductTaxAmount { get; set; }
        public decimal ProductTotal { get; set; }
        public decimal InvoiceUnitePrice { get; set; }
        public decimal InvoiceTaxPrice { get; set; }
        public decimal InvoiceProductRate { get; set; }
        public decimal InvoiceProductAmount { get; set; }
        public decimal InvoiceProductSubTotalWithDiscount { get; set; }
        public decimal InvoiceProductTaxAmount { get; set; }
        public decimal InvoiceProductTotal { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class SalonObject
    {
        public string SalonVATNumber { get; set; }
        public string CustomerName { get; set; }
        public string BillingCountryName { get; set; }
        public int BillingZipCode { get; set; }
        public string BillingAddressName { get; set; }

        //order salon address
        public string CommercialRegisterNo { get; set; }
        public string SalonCountryName { get; set; }
        public string SalonStateName { get; set; }
        public string SalonCityName { get; set; }
        public string SalonZipCode { get; set; }
        public string SalonAddressName { get; set; }
    }
}

