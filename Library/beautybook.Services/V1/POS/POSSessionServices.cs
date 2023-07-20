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
	public class POSSessionServices : AbstractPOSSessionServices
	{
		private AbstractPOSSessionDao abstractPOSSessionDao;
		private AbstractPOSOpeningCashDao abstractPOSOpeningCashDao;

		public POSSessionServices(AbstractPOSSessionDao abstractPOSSessionDao , AbstractPOSOpeningCashDao abstractPOSOpeningCashDao)
		{
			this.abstractPOSSessionDao = abstractPOSSessionDao;
			this.abstractPOSOpeningCashDao = abstractPOSOpeningCashDao;
		}

		public override PagedList<AbstractPOSSession> POSSession_BySalonId(PageParam pageParam, string search, long SalonId)
		{
			return this.abstractPOSSessionDao.POSSession_BySalonId(pageParam, search, SalonId);
		}
		
		public override SuccessResult<AbstractPOSSession> POSSession_ById(long Id)
		{
			return this.abstractPOSSessionDao.POSSession_ById(Id);
		}
		
		public override SuccessResult<AbstractPOSSession> POSSession_TopBySalonId(long SalonId)
		{
			return this.abstractPOSSessionDao.POSSession_TopBySalonId(SalonId);
		}
		
		public override SuccessResult<AbstractPOSSession> POSSession_Create(long Id, long SalonId, bool IsNewSessionCreate , string CoinsAmountStr)
		{
			var response = this.abstractPOSSessionDao.POSSession_Create(Id, SalonId, IsNewSessionCreate);

			if(response.Item.Id > 0 && CoinsAmountStr != null && CoinsAmountStr != "")
			{
				response.Item.OpeningCoinsRoot =  JsonConvert.DeserializeObject<List<OpeningCoinsRoot>>(Convert.ToString(CoinsAmountStr));

				for (int i=0; i < response.Item.OpeningCoinsRoot.Count; i++)
                {
					POSOpeningCash pOSOpeningCash = new POSOpeningCash();
					pOSOpeningCash.POSSessionId = response.Item.Id;
					pOSOpeningCash.CoinsBillsId = response.Item.OpeningCoinsRoot[i].CoinsBilllsId;
					pOSOpeningCash.Qty = Convert.ToInt64(response.Item.OpeningCoinsRoot[i].Qty);
					abstractPOSOpeningCashDao.POSOpeningCash_Add(pOSOpeningCash);
				}
            }

			return response;
		}
		public override SuccessResult<AbstractUsers> POSSession_Authentication(string Email , long SessionId)
		{
			return this.abstractPOSSessionDao.POSSession_Authentication(Email, SessionId);
		}
		public override SuccessResult<AbstractPOSSession> POSSessionClosing_CachAndAt(long Id)
		{
			return this.abstractPOSSessionDao.POSSessionClosing_CachAndAt(Id);
		}
	}

}