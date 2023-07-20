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
    public abstract class AbstractUserServicesDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractUserServices> UserServices_SalonServicesById(PageParam pageParam, string search,long SalonServicesById);
        public abstract SuccessResult<AbstractUserServices> UserServices_Upsert(AbstractUserServices abstractUserServices);
        public abstract SuccessResult<AbstractUserServices> UserServices_Delete(long Id, long DeletedBy);

    }
}
