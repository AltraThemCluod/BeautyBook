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
    public class OrderStatusTrackingServices : AbstractOrderStatusTrackingServices
    {
        private AbstractOrderStatusTrackingDao abstractOrderStatusTrackingDao;

        public OrderStatusTrackingServices(AbstractOrderStatusTrackingDao abstractOrderStatusTrackingDao)
        {
            this.abstractOrderStatusTrackingDao = abstractOrderStatusTrackingDao;
        }
        public override SuccessResult<AbstractOrderStatusTracking> OrderStatusTracking_ByOrderId(long OrderId)
        {
            return this.abstractOrderStatusTrackingDao.OrderStatusTracking_ByOrderId(OrderId);
        }
        public override SuccessResult<AbstractOrderStatusTracking> OrderStatusTracking_Upsert(AbstractOrderStatusTracking abstractOrderStatusTracking)
        {
            return this.abstractOrderStatusTrackingDao.OrderStatusTracking_Upsert(abstractOrderStatusTracking);
        }
    }

}