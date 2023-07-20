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
    public class SendSmsMarketingTemplatesServices : AbstractSendSmsMarketingTemplatesServices
    {
        private AbstractSendSmsMarketingTemplatesDao abstractSendSmsMarketingTemplatesDao;

        public SendSmsMarketingTemplatesServices(AbstractSendSmsMarketingTemplatesDao abstractSendSmsMarketingTemplatesDao)
        {
            this.abstractSendSmsMarketingTemplatesDao = abstractSendSmsMarketingTemplatesDao;
        }

        public override SuccessResult<AbstractSendSmsMarketingTemplates> SendSMSMarketingTemplates_Upsert(AbstractSendSmsMarketingTemplates abstractSendSmsMarketingTemplates)
        {
            return this.abstractSendSmsMarketingTemplatesDao.SendSMSMarketingTemplates_Upsert(abstractSendSmsMarketingTemplates);
        }

       

        public override PagedList<AbstractSendSmsMarketingTemplates> SMSUser_All(PageParam pageParam, string search, string CustomerSinceStartDate, string CustomerSinceEndDate, long MinAppoinment, long MaxAppoinment, long MinAge, long MaxAge, string LastVisitStartDate, string LastVisitEndDate, string ServicesIds,long IsAllCustomer)
        {
            return this.abstractSendSmsMarketingTemplatesDao.SMSUser_All(pageParam, search, CustomerSinceStartDate, CustomerSinceEndDate, MinAppoinment, MaxAppoinment, MinAge, MaxAge, LastVisitStartDate, LastVisitEndDate, ServicesIds, IsAllCustomer);
        }
    }

}