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

namespace BeautyBook.Services.V1
{
	public class POSOpeningCashServices : AbstractPOSOpeningCashServices
	{
		private AbstractPOSOpeningCashDao abstractPOSOpeningCashDao;

		public POSOpeningCashServices(AbstractPOSOpeningCashDao abstractPOSOpeningCashDao)
		{
			this.abstractPOSOpeningCashDao = abstractPOSOpeningCashDao;
		}
		public override SuccessResult<AbstractPOSOpeningCash> POSOpeningCash_Add(AbstractPOSOpeningCash abstractPOSOpeningCash)
		{
			return this.abstractPOSOpeningCashDao.POSOpeningCash_Add(abstractPOSOpeningCash);
		}
	}

}