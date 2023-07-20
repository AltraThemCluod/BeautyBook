using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Services.Contract
{
	public abstract class AbstractPOSPaymentServices
	{
		public abstract SuccessResult<AbstractPOSPayment> POSPayment_ById(int Id);
		public abstract SuccessResult<AbstractPosInvoice> PosInvoice_ById(int Id);
		public abstract SuccessResult<AbstractPosInvoice> PosReturnInvoice_ById(int Id);
		public abstract SuccessResult<AbstractPOSPayment> POSPayment_Upsert(AbstractPOSPayment abstractPOSPayment);
		public abstract SuccessResult<AbstractPosReturnInvoice> PosReturnInvoice_Update(AbstractPosReturnInvoice abstractPosReturnInvoice);
		public abstract PagedList<AbstractPosInvoice> PosInvoice_All(PageParam pageParam, string search, long SalonId, string InvoiceDate, string InvoiceNumber, long CustomerId);
	}
}
