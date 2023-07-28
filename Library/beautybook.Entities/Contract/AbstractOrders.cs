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
    public abstract class AbstractOrders
    {
        public long Id { get; set; }
        public long VendorId { get; set; }
        public long SalonId { get; set; }
        public string SalonName { get; set; }
        public string PrimaryPhone { get; set; }
        public string AddressLine1 { get; set; }
        public long OrignalInvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public string OrderNo { get; set; }
        public string InvoiceNo { get; set; }
        public string OrderDate { get; set; }
        public long Payment { get; set; }
        public string PaymentTypeName { get; set; }
        public long MasterPaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public long ShippingAddressId { get; set; }
        public long IsBillingAddress { get; set; }
        public long BillingAddressId { get; set; }
        public long ShippingAddress { get; set; }
        public string ShippingAddressName { get; set; }
        public long ShippingSalonId { get; set; }
        public string ShippingCustomerName { get; set; }
        public long ShippingCountryId { get; set; }
        public string ShippingCountryName { get; set; }
        public long ShippingStateId { get; set; }
        public string ShippingStateName { get; set; }
        public string ShippingCityName { get; set; }
        public long BillingAddress { get; set; }
        public long BillingZipCode { get; set; }
        public string BillingAddressName { get; set; }
        public long BillingSalonId { get; set; }
        public string BillingCustomerName { get; set; }
        public long BillingCountryId { get; set; }
        public string BillingCountryName { get; set; }
        public long BillingStateId { get; set; }
        public string BillingStateName { get; set; }
        public string BillingCityName { get; set; }
        public long ProceedStatus { get; set; }
        public string ProceedStatusName { get; set; }
        public long LookUpStatusId { get; set; }
        public string LookUpStatusName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public string DateOfOrder { get; set; }
        public List<OrderProducts> OrderProducts { get; set; }
        public List<OrderStatusTracking> OrderStatusTracking { get; set; }

        public List<OrderProductRoot> ProductDetails { get; set; }
        public string ProductDetailsStr { get; set; }

        public string LookUpStatus { get; set; }

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductRate { get; set; }
        public decimal ProductTax { get; set; }
        public long Qty { get; set; }
        public decimal ProductAmount { get; set; }
        public decimal ProductSubTotal { get; set; }
        public decimal ProductTaxAmount { get; set; }
        public decimal ProductTotal { get; set; }
        public string AddToCartIdStr { get; set; }
        public string VendorIdStr { get; set; }
        public long TotalItems { get; set; }
        public string InvoicePrintDate { get; set; }
        public string SalonVATNumber { get; set; }  

        //Order salon data 
        public string CommercialRegisterNo { get; set; }
        public string SalonCountryName { get; set; }
        public string SalonStateName { get; set; }
        public string SalonCityName { get; set; }
        public string SalonZipCode { get; set; }
        public string SalonAddressName { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate?.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate?.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

public class OrderProductRoot
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
    public string ProductImage { get; set; }
}

public class MyPayload
{
    public int respStatus { get; set; }
}

public abstract class AbstractOrderInvoice
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long OrderId { get; set; }
    public string Supplername { get; set; }
    public string VendorCompanyName { get; set; }
    public string CommercialRegisterNumber { get; set; }
    public string VATNumber { get; set; }
    public string SupplerCountryName { get; set; }
    public string SupplerStateName { get; set; }
    public string SupplerCityName { get; set; }
    public string SupplerZipCode { get; set; }
    public string SupplerAddress { get; set; }
    public string InvoicePrintDate { get; set; }
    public string OrderDate { get; set; }
    public string InvoiceNo { get; set; }
    public string OrderObjectStr { get; set; }
    public string SalonObjectStr { get; set; }
    public DateTime CreatedDate { get; set; }
    public long ParentId { get; set; }
    public string  CustomerName { get; set; }
    public string OldInvoiceNumber { get; set; }
    public List<OrderItemData> OrderItemData { get; set; }
    public List<OrderSalonData> OrderSalonData { get; set; }

}

public abstract class AbstractOrderInvoiceSalon
{
    public long CustomerId { get; set; }
    public string CustomerName { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class OrderItemData
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
    public decimal ProductTaxAmount { get; set; }
    public decimal ProductTotal { get; set; }
    public decimal InvoiceUnitePrice { get; set; }
    public decimal InvoiceTaxPrice { get; set; }
    public decimal InvoiceProductRate { get; set; }
    public decimal InvoiceProductAmount { get; set; }
    public decimal ProductDiscount { get;set; }
    public decimal InvoiceProductDiscount { get; set; }
    public decimal InvoiceProductSubTotalWithDiscount { get; set; }
    public decimal InvoiceProductTaxAmount { get; set; }
    public decimal InvoiceProductTotal { get; set; }
    public DateTime CreatedDate { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
//public class OrderSalonData
//{
//    public string SalonVATNumber { get; set; }
//    public string CustomerName { get; set; }
//    public string BillingCountryName { get; set; }
//    public int BillingZipCode { get; set; }
//    public string BillingAddressName { get; set; }
//    public string CommercialRegisterNo { get; set; }
//}

public class OrderSalonData
{
    public string SalonVATNumber { get; set; }
    public string CustomerName { get; set; }
    public string CommercialRegisterNo { get; set; }
    public string SalonCountryName { get; set; }
    public string SalonStateName { get; set; }
    public string SalonCityName { get; set; }
    public int SalonZipCode { get; set; }
    public string SalonAddressName { get; set; }
}

public abstract class AbstractOrderReturnInvoice
{
    public long Id { get; set; }
    public string OrderObjectStr { get; set; }
}