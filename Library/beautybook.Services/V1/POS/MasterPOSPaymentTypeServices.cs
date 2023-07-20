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
	public class MasterPOSPaymentTypeServices : AbstractMasterPOSPaymentTypeServices
	{
		private AbstractMasterPOSPaymentTypeDao abstractMasterPOSPaymentTypeDao;

		public MasterPOSPaymentTypeServices(AbstractMasterPOSPaymentTypeDao abstractMasterPOSPaymentTypeDao)
		{
			this.abstractMasterPOSPaymentTypeDao = abstractMasterPOSPaymentTypeDao;
		}

		public override PagedList<AbstractMasterPOSPaymentType> MasterPOSPaymentType_All(PageParam pageParam, string search)
		{
			return this.abstractMasterPOSPaymentTypeDao.MasterPOSPaymentType_All(pageParam, search);
		}
		
	}

}