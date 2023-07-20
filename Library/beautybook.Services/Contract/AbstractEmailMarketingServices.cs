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
    public abstract class AbstractEmailMarketingServices
    {
        public abstract PagedList<AbstractEmailMarketing> EmailMarketing_All(PageParam pageParam, string search, long CreatedBy,long SalonId);
        public abstract SuccessResult<AbstractEmailMarketing> EmailMarketing_ById(long Id);
        public abstract SuccessResult<AbstractEmailMarketing> EmailMarketing_Upsert(AbstractEmailMarketing abstractEmailMarketing,long NoOfEmailCount);
        //public abstract SuccessResult<AbstractEmailMarketing> UserEmailMarketing_Upsert(AbstractEmailMarketing abstractEmailMarketing);

    }
}
