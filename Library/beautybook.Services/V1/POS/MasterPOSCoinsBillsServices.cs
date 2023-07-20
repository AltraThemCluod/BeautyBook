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
	public class MasterPOSCoinsBillsServices : AbstractMasterPOSCoinsBillsServices
	{
		private AbstractMasterPOSCoinsBillsDao abstractMasterPOSCoinsBillsDao;

		public MasterPOSCoinsBillsServices(AbstractMasterPOSCoinsBillsDao abstractMasterPOSCoinsBillsDao)
		{
			this.abstractMasterPOSCoinsBillsDao = abstractMasterPOSCoinsBillsDao;
		}

		public override PagedList<AbstractMasterPOSCoinsBills> MasterPOSCoinsBills_All(PageParam pageParam, string search)
		{
			return this.abstractMasterPOSCoinsBillsDao.MasterPOSCoinsBills_All(pageParam, search);
		}
		
	}

}