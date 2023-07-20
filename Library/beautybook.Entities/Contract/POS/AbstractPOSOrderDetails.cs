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
	public abstract class AbstractPOSOrderDetails
	{
		public long Id { get; set; }
		public long POSDetailsId { get; set; }
		public long ServiceId { get; set; }
		public long POSTypeId { get; set; }
		public long CategoryId { get; set; }
		public long AssignUserId { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedAt { get; set; }
		public long CreatedBy { get; set; }
		public DateTime UpdatedAt { get; set; }
		public long UpdatedBy { get; set; }
		public DateTime DeletedAt { get; set; }
		public long DeletedBy { get; set; }
		public decimal Price { get; set; }

		[NotMapped]
		public string CreatedDateStr => CreatedAt != null ? CreatedAt.ToString("dd-MMM-yyyy hh:mm tt") : "-";
		[NotMapped]
		public string UpdatedDateStr => UpdatedAt != null ? UpdatedAt.ToString("dd-MMM-yyyy hh:mm tt") : "-";
	}
}

