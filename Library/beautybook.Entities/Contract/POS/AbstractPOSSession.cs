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
	public abstract class AbstractPOSSession
	{
		public long Id { get; set; }
		public long SalonId { get; set; }
		public long UserId { get; set; }
		public long TotalOrderSales { get; set; }
		public string SessionHandlerUserName { get; set; }
		public DateTime? LastClosingAt { get; set; }
		public decimal LastClosingCash { get; set; }
		public decimal LastClosingOnline { get; set; }
		public DateTime? CreatedSessionAt { get; set; }
		public DateTime? EndSessionAt { get; set; }
		public List<OpeningCoinsRoot> OpeningCoinsRoot { get; set; }

		[NotMapped]
		public string LastClosingAtStr => LastClosingAt != null ? LastClosingAt?.ToString("dd/MM/yyyy hh:mm:ss tt") : "-";
		[NotMapped]
		public string CreatedSessionAtStr => CreatedSessionAt != null ? CreatedSessionAt?.ToString("dd-MMM-yyyy") : "-";
		[NotMapped]
		public string EndSessionAtStr => EndSessionAt != null ? EndSessionAt?.ToString("dd-MMM-yyyy hh:mm tt") : "-";
	}

	public class OpeningCoinsRoot
	{
		public int CoinsBilllsId { get; set; }
		public string Qty { get; set; }
	}

}

