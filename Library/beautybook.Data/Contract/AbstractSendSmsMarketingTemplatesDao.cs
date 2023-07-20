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
    public abstract class AbstractSendSmsMarketingTemplatesDao : AbstractBaseDao
    {
        public abstract SuccessResult<AbstractSendSmsMarketingTemplates> SendSMSMarketingTemplates_Upsert(AbstractSendSmsMarketingTemplates abstractSendSmsMarketingTemplates);
        public abstract PagedList<AbstractSendSmsMarketingTemplates> SMSUser_All(PageParam pageParam, string search, string CustomerSinceStartDate,string CustomerSinceEndDate,long MinAppoinment,long MaxAppoinment,long MinAge,long MaxAge,string LastVisitStartDate,string LastVisitEndDate,string ServicesIds,long IsAllCustomer);
    }
}
