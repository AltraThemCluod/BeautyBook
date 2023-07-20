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
	public abstract class AbstractPOSOpeningCash
	{
		public long Id { get; set; }
		public long POSSessionId { get; set; }
		public long CoinsBillsId { get; set; }
		public long Qty { get; set; }
		public decimal TotalAmount { get; set; }
		public string Note { get; set; }
		public DateTime CreatedAt { get; set; }
		

		[NotMapped]
		public string CreatedDateStr => CreatedAt != null ? CreatedAt.ToString("dd-MMM-yyyy hh:mm tt") : "-";
		
	}
}

