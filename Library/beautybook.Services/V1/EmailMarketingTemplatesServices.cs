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
    public class EmailMarketingTemplatesServices : AbstractEmailMarketingTemplatesServices
    {
        private AbstractEmailMarketingTemplatesDao abstractEmailMarketingTemplatesDao;

        public EmailMarketingTemplatesServices(AbstractEmailMarketingTemplatesDao abstractEmailMarketingTemplatesDao)
        {
            this.abstractEmailMarketingTemplatesDao = abstractEmailMarketingTemplatesDao;
        }

        public override PagedList<AbstractEmailMarketingTemplates> EmailMarketingTemplates_All(PageParam pageParam, string search)
        {
            return this.abstractEmailMarketingTemplatesDao.EmailMarketingTemplates_All(pageParam, search);
        }
        public override SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates_ById(long Id)
        {
            return this.abstractEmailMarketingTemplatesDao.EmailMarketingTemplates_ById(Id);
        }
        public override SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates_Delete(long Id , long DeletedBy)
        {
            return this.abstractEmailMarketingTemplatesDao.EmailMarketingTemplates_Delete(Id , DeletedBy);
        }
        public override SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates_Upsert(AbstractEmailMarketingTemplates abstractEmailMarketingTemplates)
        {
            return this.abstractEmailMarketingTemplatesDao.EmailMarketingTemplates_Upsert(abstractEmailMarketingTemplates);
        }
    }

}