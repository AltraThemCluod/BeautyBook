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
	public class POSOrderDetailsServices : AbstractPOSOrderDetailsServices
	{
		private AbstractPOSOrderDetailsDao abstractPOSOrderDetailsDao;

		public POSOrderDetailsServices(AbstractPOSOrderDetailsDao abstractPOSOrderDetailsDao)
		{
			this.abstractPOSOrderDetailsDao = abstractPOSOrderDetailsDao;
		}

		public override PagedList<AbstractPOSOrderDetails> POSOrderDetails_All(PageParam pageParam, string search)
		{
			return this.abstractPOSOrderDetailsDao.POSOrderDetails_All(pageParam, search);
		}
		public override SuccessResult<AbstractPOSOrderDetails> POSOrderDetails_ById(int Id)
		{
			return this.abstractPOSOrderDetailsDao.POSOrderDetails_ById(Id);
		}
		public override SuccessResult<AbstractPOSOrderDetails> POSOrderDetails_Delete(int Id, int DeletedBy)
		{
			return this.abstractPOSOrderDetailsDao.POSOrderDetails_Delete(Id, DeletedBy);
		}
		public override SuccessResult<AbstractPOSOrderDetails> POSOrderDetails_Upsert(AbstractPOSOrderDetails abstractPOSOrderDetails)
		{
			return this.abstractPOSOrderDetailsDao.POSOrderDetails_Upsert(abstractPOSOrderDetails); 
		}
	}

}