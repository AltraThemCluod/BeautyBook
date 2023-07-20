using BeautyBook;
using BeautyBook.Entities.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BeautyBook.Entities.Contract
{
	public abstract class AbstractPOSPayment
	{
		public long Id { get; set; }

		public long SalonId { get; set; }
		public long POSDetailsId { get; set; }
		public decimal SubTotal { get; set; }
		public decimal DiscountSales { get; set; }
		public decimal TotalSalesTax { get; set; }
		public decimal TotalAmount { get; set; }
		public long PaymentTypeId { get; set; }
		public decimal CashAmount { get; set; }
		public decimal CareditAmount { get; set; }
		public DateTime PayAt { get; set; }
		public DateTime PayUpdateAt { get; set; }
		public long InvoiceId { get; set; }

		public string QrCodeURL { get; set; }

		[NotMapped]
		public string PayAtStr => PayAt != null ? PayAt.ToString("dd-MMM-yyyy hh:mm tt") : "-";
		[NotMapped]
		public string PayUpdateAtStr => PayUpdateAt != null ? PayUpdateAt.ToString("dd-MMM-yyyy hh:mm tt") : "-";
	}


	public abstract class AbstractPosInvoice
    {
		public long Id { get; set; }
		public long POSSessionId { get; set; }
		public long CustomerId { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal SubTotal { get; set; }
		public decimal DiscountSales { get; set; }
		public decimal TotalSalesTax { get; set; }
		public string SalonLogoUrl { get; set; }
		public string SalonName { get; set; }
		public string SalonPrimaryPhone { get; set; }
		public string AddressLine1 { get; set; }
		public string TaxNumber { get; set; }
		public string PosInvoiceNumber { get; set; }
		public string CustomerName { get; set; }
		public string CustomerPrimaryPhone { get; set; }
		public string CustomerEmail { get; set; }
		public string PosOrderInvoiceStr { get; set; }
		public List<PosInvoiceOrder> PosInvoiceOrder { get; set; }
		public DateTime CreatedDate { get; set; }
		public long CreatedBy { get; set; }
		public decimal DiscountSalesAmount { get; set;}
		public string QrCodeURL { get; set; }
		public long ParentId { get; set; }
		public bool IsSaved { get; set; }
		public string EnteredByUserName { get; set; }
		public string OldInvoiceNumber { get; set; }

		[NotMapped]
		public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
		public string InvoiceDateStr => CreatedDate != null ? CreatedDate.ToString("dd/MM/yyyy" , CultureInfo.InvariantCulture) : "-";
	}

	public class PosInvoiceOrder
	{
		public int Id { get; set; }
		public int POSDetailsId { get; set; }
		public int ServiceId { get; set; }
		public int POSTypeId { get; set; }
		public int CategoryId { get; set; }
		public int AssignUserId { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedAt { get; set; }
		public int CreatedBy { get; set; }
		public int DeletedBy { get; set; }
		public double Price { get; set; }
		public string Name { get; set; }
		public string ServiceProvider { get; set; }
	}

	public abstract class AbstractPosReturnInvoice
	{
		public long Id { get; set; }
		public long SalonId { get; set; }
		public string PosOrderInvoiceStr { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal SubTotal { get; set; }
		public decimal TotalSalesTax { get; set; }
		public string QrCodeURL { get; set; }
	}
}

