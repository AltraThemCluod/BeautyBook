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
	public abstract class AbstractPOSOrderDetailsDao : AbstractBaseDao
	{

		public abstract PagedList<AbstractPOSOrderDetails> POSOrderDetails_All(PageParam pageParam, string search);
		public abstract SuccessResult<AbstractPOSOrderDetails> POSOrderDetails_ById(int Id);
		public abstract SuccessResult<AbstractPOSOrderDetails> POSOrderDetails_Delete(int Id, int DeletedBy);
		public abstract SuccessResult<AbstractPOSOrderDetails> POSOrderDetails_Upsert(AbstractPOSOrderDetails abstractPOSOrderDetails);
	}
}
