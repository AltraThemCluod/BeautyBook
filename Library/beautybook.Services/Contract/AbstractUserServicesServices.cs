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
    public abstract class AbstractUserServicesServices
    {
        public abstract PagedList<AbstractUserServices> UserServices_SalonServicesById(PageParam pageParam, string search, long SalonServicesById);
        public abstract SuccessResult<AbstractUserServices> UserServices_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractUserServices> UserServices_Upsert(AbstractUserServices abstractUserServices);

    }
}
