using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;
using Newtonsoft.Json;

namespace BeautyBook.Services.V1
{
	public class POSPaymentServices : AbstractPOSPaymentServices
	{
		private AbstractPOSPaymentDao abstractPOSPaymentDao;

		public POSPaymentServices(AbstractPOSPaymentDao abstractPOSPaymentDao)
		{
			this.abstractPOSPaymentDao = abstractPOSPaymentDao;
		}

		public override SuccessResult<AbstractPOSPayment> POSPayment_ById(int Id)
		{
			return this.abstractPOSPaymentDao.POSPayment_ById(Id);
		}

		public override SuccessResult<AbstractPosInvoice> PosInvoice_ById(int Id)
		{
			var response = this.abstractPOSPaymentDao.PosInvoice_ById(Id);

			if (response.Item != null)
			{
				response.Item.PosInvoiceOrder = JsonConvert.DeserializeObject<List<PosInvoiceOrder>>(response.Item.PosOrderInvoiceStr);
			}

			return response;
		}

		public override SuccessResult<AbstractPosInvoice> PosReturnInvoice_ById(int Id)
		{
			return this.abstractPOSPaymentDao.PosReturnInvoice_ById(Id);
		}

		public override SuccessResult<AbstractPOSPayment> POSPayment_Upsert(AbstractPOSPayment abstractPOSPayment)
		{
			return this.abstractPOSPaymentDao.POSPayment_Upsert(abstractPOSPayment);
		}

		public override SuccessResult<AbstractPosReturnInvoice> PosReturnInvoice_Update(AbstractPosReturnInvoice abstractPosReturnInvoice)
		{
			return this.abstractPOSPaymentDao.PosReturnInvoice_Update(abstractPosReturnInvoice);
		}

		public override PagedList<AbstractPosInvoice> PosInvoice_All(PageParam pageParam, string search, long SalonId, string InvoiceDate, string InvoiceNumber, long CustomerId)
		{
			var response = this.abstractPOSPaymentDao.PosInvoice_All(pageParam, search, SalonId, InvoiceDate, InvoiceNumber, CustomerId);

			if (response.TotalRecords > 0)
			{
                for (int i = 0; i < response.TotalRecords; i++)
                {
					response.Values[i].PosInvoiceOrder = JsonConvert.DeserializeObject<List<PosInvoiceOrder>>(response.Values[i].PosOrderInvoiceStr);
				}
			}

			return response;
		}
	}

}