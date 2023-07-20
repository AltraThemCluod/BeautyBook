using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
using Newtonsoft.Json;

namespace BeautyBook.Services.V1
{
	public class POSDetailsServices : AbstractPOSDetailsServices
	{
		private AbstractPOSDetailsDao abstractPOSDetailsDao;
		private AbstractPOSOrderDetailsDao abstractPOSOrderDetailsDao;

		public POSDetailsServices(AbstractPOSDetailsDao abstractPOSDetailsDao , AbstractPOSOrderDetailsDao abstractPOSOrderDetailsDao)
		{
			this.abstractPOSDetailsDao = abstractPOSDetailsDao;
			this.abstractPOSOrderDetailsDao = abstractPOSOrderDetailsDao;
		}
		
		public override PagedList<AbstractPOSDetails> POSDetails_All(PageParam pageParam, string search , long SalonId, long POSSessionId)
		{
			return this.abstractPOSDetailsDao.POSDetails_All(pageParam, search, SalonId, POSSessionId);
		}
		public override PagedList<AbstractPosOffer> PosOffer_All(long OfferId, long SalonId)
		{
			return this.abstractPOSDetailsDao.PosOffer_All(OfferId, SalonId);
		}
		public override PagedList<AbstractPOSAssignEmployee> POSAssignEmployee_All(long LookUpUserTypeId, long SalonId)
		{
			return this.abstractPOSDetailsDao.POSAssignEmployee_All(LookUpUserTypeId, SalonId);
		}
		public override SuccessResult<AbstractPOSDetails> POSDetails_ById(int Id)
		{
			var response = this.abstractPOSDetailsDao.POSDetails_ById(Id);

			if (response.Item != null)
			{
				response.Item.PosOrderInvoice = JsonConvert.DeserializeObject<List<PosOrderInvoice>>(response.Item.PosOrderInvoiceStr);
			}

			return response;
		}
		public override SuccessResult<AbstractPOSDetails> POSDetails_Delete(int Id, int DeletedBy)
		{
			return this.abstractPOSDetailsDao.POSDetails_Delete(Id, DeletedBy);
		}
		public override SuccessResult<AbstractPOSDetails> POSDetails_Upsert(AbstractPOSDetails abstractPOSDetails)
		{
			var response =  this.abstractPOSDetailsDao.POSDetails_Upsert(abstractPOSDetails);

			if (response.Item != null)
			{
				response.Item.ServiceDetails = JsonConvert.DeserializeObject<List<PosOrderDetailsRoot>>(abstractPOSDetails.ServiceDetailsStr);

                for (int i = 0; i < response.Item.ServiceDetails.Count; i++)
                {
					POSOrderDetails pOSOrderDetails = new POSOrderDetails();
					pOSOrderDetails.POSDetailsId = response.Item.Id;
					pOSOrderDetails.ServiceId = response.Item.ServiceDetails[i].ServiceId;
					pOSOrderDetails.POSTypeId = response.Item.ServiceDetails[i].PosType;
					pOSOrderDetails.CategoryId = response.Item.ServiceDetails[i].CategoryId;
					pOSOrderDetails.Price = response.Item.ServiceDetails[i].Price;
					pOSOrderDetails.AssignUserId = response.Item.ServiceDetails[i].AssignToUser;
					pOSOrderDetails.CreatedBy = response.Item.CreatedBy;

					abstractPOSOrderDetailsDao.POSOrderDetails_Upsert(pOSOrderDetails);
				}
			}

			return response;
		}
	}
}