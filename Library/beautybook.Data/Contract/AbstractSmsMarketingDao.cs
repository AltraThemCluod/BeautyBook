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
    public abstract class AbstractSmsMarketingDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractSmsMarketing> SmsMarketing_All(PageParam pageParam, string search, long CreatedBy,long SalonId);
        public abstract SuccessResult<AbstractSmsMarketing> SmsMarketing_ById(long Id);
        public abstract SuccessResult<AbstractSmsMarketing> SmsMarketing_Upsert(AbstractSmsMarketing abstractSmsMarketing,long NoOfSMSCount);
        public abstract SuccessResult<AbstractSmsMarketing> UserSMSMarketing_Upsert(long Id, long SMSMarketingId, string UserIds, string Body);

    }
}
