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
	public abstract class AbstractPOSSessionDao : AbstractBaseDao
	{
		public abstract PagedList<AbstractPOSSession> POSSession_BySalonId(PageParam pageParam, string search, long SalonId);
		public abstract SuccessResult<AbstractPOSSession> POSSession_ById(long Id);
		public abstract SuccessResult<AbstractPOSSession> POSSession_TopBySalonId(long SalonId);
		public abstract SuccessResult<AbstractPOSSession> POSSession_Create(long Id, long SalonId, bool IsNewSessionCreate);
		public abstract SuccessResult<AbstractUsers> POSSession_Authentication(string Email,long SessionId);
		public abstract SuccessResult<AbstractPOSSession> POSSessionClosing_CachAndAt(long Id);
		
	}
}
