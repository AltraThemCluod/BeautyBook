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
    public abstract class AbstractEmailMarketingTemplatesServices
    {
        public abstract PagedList<AbstractEmailMarketingTemplates> EmailMarketingTemplates_All(PageParam pageParam, string search);
        public abstract SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates_ById(long Id);
        public abstract SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates_Delete(long Id,long DeletedBy);
        public abstract SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates_Upsert(AbstractEmailMarketingTemplates abstractEmailMarketingTemplates);
    }
}
