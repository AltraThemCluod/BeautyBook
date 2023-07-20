using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Services.Contract
{
    public abstract class AbstractMasterOrderStatusServices
    {
        public abstract SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_Upsert(AbstractMasterOrderStatus abstractMasterOrderStatus);
        public abstract SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_ById(long Id);
        public abstract PagedList<AbstractMasterOrderStatus> MasterOrderStatus_All(PageParam pageParam, string search);
        public abstract SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_ActInAct(long Id);

    }
}
