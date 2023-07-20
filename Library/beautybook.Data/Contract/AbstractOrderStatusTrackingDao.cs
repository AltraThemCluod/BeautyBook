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
    public abstract class AbstractOrderStatusTrackingDao : AbstractBaseDao
    {
        public abstract SuccessResult<AbstractOrderStatusTracking> OrderStatusTracking_ByOrderId(long OrderId);
        public abstract SuccessResult<AbstractOrderStatusTracking> OrderStatusTracking_Upsert(AbstractOrderStatusTracking abstractAbstractOrderStatusTracking);
    }
}
