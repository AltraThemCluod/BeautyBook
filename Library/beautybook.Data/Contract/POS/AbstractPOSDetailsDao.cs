using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Data.Contract
{
	public abstract class AbstractPOSDetailsDao : AbstractBaseDao
	{
		public abstract PagedList<AbstractPOSDetails> POSDetails_All(PageParam pageParam, string search,long SalonId, long POSSessionId);
		public abstract PagedList<AbstractPosOffer> PosOffer_All(long OfferId,long SalonId);
		public abstract PagedList<AbstractPOSAssignEmployee> POSAssignEmployee_All(long LookUpUserTypeId, long SalonId);
		public abstract SuccessResult<AbstractPOSDetails> POSDetails_ById(int Id);
		public abstract SuccessResult<AbstractPOSDetails> POSDetails_Delete(int Id ,int DeletedBy);
		public abstract SuccessResult<AbstractPOSDetails> POSDetails_Upsert(AbstractPOSDetails abstractPOSDetails);
	}
}
