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
    public abstract class AbstractOfflineVendorDao
    {
        public abstract PagedList<AbstractOfflineVendor> OfflineVendor_All(PageParam pageparam, string search, long SalonId);

        public abstract SuccessResult<AbstractOfflineVendor> OfflineVendor_Upsert(AbstractOfflineVendor abstractOfflineVendor);
        public abstract SuccessResult<AbstractOfflineVendor> OfflineVendor_ById(long Id);


    }
}
