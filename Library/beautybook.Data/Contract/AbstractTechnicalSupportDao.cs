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
    public abstract class AbstractTechnicalSupportDao
    {
        public abstract SuccessResult<AbstractTechnicalSupport> TechnicalSupport_Upsert(AbstractTechnicalSupport abstractTechnicalSupport);
        public abstract PagedList<AbstractTechnicalSupport> TechnicalSupport_All(PageParam pageParam, string search,long UserId,long UserTypeId, long SalonId);
        public abstract SuccessResult<AbstractTechnicalSupport> TechnicalSupport_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractTechnicalSupport> TechnicalSupport_ById(long Id);

    }
}
