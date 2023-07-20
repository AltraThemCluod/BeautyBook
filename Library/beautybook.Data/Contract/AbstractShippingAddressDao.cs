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
    public abstract class AbstractShippingAddressDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractShippingAddress> ShippingAddress_All(PageParam pageParam, string search,long SalonId);
        public abstract SuccessResult<AbstractShippingAddress> ShippingAddress_Upsert(AbstractShippingAddress abstractShippingAddress);
        public abstract SuccessResult<AbstractShippingAddress> ShippingAddress_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractShippingAddress> ShippingAddress_ById(long Id);
    }
}
