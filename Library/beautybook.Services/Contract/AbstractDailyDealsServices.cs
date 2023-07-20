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
    public abstract class AbstractDailyDealsServices
    {
        public abstract SuccessResult<AbstractDailyDeals> DailyDeals_Delete(long Id,long DeletedBy);
        public abstract PagedList<AbstractDailyDeals> DailyDeals_All(PageParam pageParam, string search,long SalonId);
        public abstract SuccessResult<AbstractDailyDeals> DailyDeals_ById(long Id);
        public abstract SuccessResult<AbstractDailyDeals> DailyDeals_Upsert(AbstractDailyDeals abstractDailyDeals);
    }
}
