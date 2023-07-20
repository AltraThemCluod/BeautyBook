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
    public class MasterOrderStatusServices : AbstractMasterOrderStatusServices
    {
        private AbstractMasterOrderStatusDao abstractMasterOrderStatusDao;

        public MasterOrderStatusServices(AbstractMasterOrderStatusDao abstractMasterOrderStatusDao)
        {
            this.abstractMasterOrderStatusDao = abstractMasterOrderStatusDao;
        }

        public override SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_Upsert(AbstractMasterOrderStatus abstractMasterOrderStatus)
        {
            return this.abstractMasterOrderStatusDao.MasterOrderStatus_Upsert(abstractMasterOrderStatus);
        }

        public override SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_ById(long Id)
        {
            return this.abstractMasterOrderStatusDao.MasterOrderStatus_ById(Id);
        }

        public override PagedList<AbstractMasterOrderStatus> MasterOrderStatus_All(PageParam pageParam, string search)
        {
            return this.abstractMasterOrderStatusDao.MasterOrderStatus_All(pageParam, search);

        }
        public override SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_ActInAct(long Id)
        {
            return this.abstractMasterOrderStatusDao.MasterOrderStatus_ActInAct(Id);
        }

        public override SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_Delete(long Id, long DeletedBy)
        {
            return this.abstractMasterOrderStatusDao.MasterOrderStatus_Delete(Id, DeletedBy);
        }

    }
}
