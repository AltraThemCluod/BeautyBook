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
    public abstract class AbstractDailyDealsDao : AbstractBaseDao
    {
        public abstract SuccessResult<AbstractDailyDeals> DailyDeals_Delete(long Id,long DeletedBy);
        public abstract PagedList<AbstractDailyDeals> DailyDeals_All(PageParam pageParam, string search,long SalonId);
        public abstract SuccessResult<AbstractDailyDeals> DailyDeals_ById(long Id);
        public abstract SuccessResult<AbstractDailyDeals> DailyDeals_Upsert(AbstractDailyDeals abstractDailyDeals);
    }
}
