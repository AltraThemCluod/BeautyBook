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
    public class SendEmailMarketingTemplatesServices : AbstractSendEmailMarketingTemplatesServices
    {
        private AbstractSendEmailMarketingTemplatesDao abstractSendEmailMarketingTemplatesDao;

        public SendEmailMarketingTemplatesServices(AbstractSendEmailMarketingTemplatesDao abstractSendEmailMarketingTemplatesDao)
        {
            this.abstractSendEmailMarketingTemplatesDao = abstractSendEmailMarketingTemplatesDao;
        }

        public override SuccessResult<AbstractSendEmailMarketingTemplates> SendEmailMarketingTemplates_Upsert(AbstractSendEmailMarketingTemplates abstractSendEmailMarketingTemplates)
        {
            return this.abstractSendEmailMarketingTemplatesDao.SendEmailMarketingTemplates_Upsert(abstractSendEmailMarketingTemplates);
        }

       

        public override PagedList<AbstractSendEmailMarketingTemplates> EmailUser_All(PageParam pageParam, string search, string CustomerSinceStartDate, string CustomerSinceEndDate, long MinAppoinment, long MaxAppoinment, long MinAge, long MaxAge, string LastVisitStartDate, string LastVisitEndDate, string ServicesIds,long IsAllCustomer,long SalonId)
        {
            return this.abstractSendEmailMarketingTemplatesDao.EmailUser_All(pageParam, search, CustomerSinceStartDate, CustomerSinceEndDate, MinAppoinment, MaxAppoinment, MinAge, MaxAge, LastVisitStartDate, LastVisitEndDate, ServicesIds, IsAllCustomer, SalonId);
        }
    }

}