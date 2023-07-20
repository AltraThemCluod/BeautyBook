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
    public abstract class AbstractSmsMarketingServices
    {
        public abstract PagedList<AbstractSmsMarketing> SmsMarketing_All(PageParam pageParam, string search, long CreatedBy,long SalonId);
        public abstract SuccessResult<AbstractSmsMarketing> SmsMarketing_ById(long Id);
        public abstract SuccessResult<AbstractSmsMarketing> SmsMarketing_Upsert(AbstractSmsMarketing abstractSmsMarketing,long NoOfSMSCount);
    }
}
