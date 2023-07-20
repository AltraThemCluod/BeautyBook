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
    public class DailyDealsServices : AbstractDailyDealsServices
    {
        private AbstractDailyDealsDao abstractDailyDealsDao;

        public DailyDealsServices(AbstractDailyDealsDao abstractDailyDealsDao)
        {
            this.abstractDailyDealsDao = abstractDailyDealsDao;
        }

        public override PagedList<AbstractDailyDeals> DailyDeals_All(PageParam pageParam, string search,long SalonId)
        {
            return this.abstractDailyDealsDao.DailyDeals_All(pageParam, search, SalonId);
        }
        public override SuccessResult<AbstractDailyDeals> DailyDeals_Delete(long Id,long DeletedBy)
        {
            return this.abstractDailyDealsDao.DailyDeals_Delete(Id, DeletedBy);
        }
        public override SuccessResult<AbstractDailyDeals> DailyDeals_ById(long Id)
        {
            return this.abstractDailyDealsDao.DailyDeals_ById(Id);
        }
        public override SuccessResult<AbstractDailyDeals> DailyDeals_Upsert(AbstractDailyDeals abstractDailyDeals)
        {
            return this.abstractDailyDealsDao.DailyDeals_Upsert(abstractDailyDeals); ;
        }
        
    }

}