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
	public abstract class AbstractPOSDetails
	{
		public long Id { get; set; }
		public long POSSessionId { get; set; }
		public long POSInvoiceId { get; set; }
		public long POSTypeId { get; set; }
		public long CategoryId { get; set; }
		public string ServiceIds { get; set; }
		public long CustomerId { get; set; }
		public bool IsDeleted { get; set; }
		public string ServiceDetailsStr { get; set; }
		public string PosPaymentDetails { get; set; }
		public List<PosOrderDetailsRoot> ServiceDetails { get; set; }
		public DateTime CreatedAt { get; set; }
		public long CreatedBy { get; set; }
		public DateTime UpdatedAt { get; set; }
		public long UpdatedBy { get; set; }
		public DateTime DeletedAt { get; set; }
		public long DeletedBy { get; set; }
		public string POSInvoiceNumber { get; set; }
		public string CustomerName { get; set; }
		public decimal TotalAmount { get; set; }
		public string PosOrderInvoiceStr { get; set; }
		public List<PosOrderInvoice> PosOrderInvoice { get; set; }

		[NotMapped]
		public string CreatedDateStr => CreatedAt != null ? CreatedAt.ToString("dd-MMM-yyyy hh:mm tt") : "-";
		[NotMapped]
		public string UpdatedDateStr => UpdatedAt != null ? UpdatedAt.ToString("dd-MMM-yyyy hh:mm tt") : "-";
	}


	// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
	public class PosOrderDetailsRoot
	{
		public int PosType { get; set; }
		public int ServiceId { get; set; }
		public int CategoryId { get; set; }
		public string LookUpServicesName { get; set; }
		public int AssignToUser { get; set; }
		public int Price { get; set; }
	}


	public abstract class AbstractPosOffer
	{
		public long Id { get; set; }
		public long OfferId { get; set; }
		public long SalonId { get; set; }
		public long LookUpServicesId { get; set; }
		public string ServicePhoto { get; set; }
		public decimal OfferPrice { get; set; }
		public DateTime CreatedDate { get; set; }
		public long CreatedBy { get; set; }
		public string ServiceName { get; set; }
    }

	public class PosOrderInvoice
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
		public decimal Price { get; set; }
		public string Name { get; set; }
		public string ServPhotoUrl { get;set; }
		public string AssignUserName { get; set; }
	}

	public abstract class AbstractPOSAssignEmployee
    {
		public int Id { get; set; }
		public string UserName { get; set; }
	}
}

