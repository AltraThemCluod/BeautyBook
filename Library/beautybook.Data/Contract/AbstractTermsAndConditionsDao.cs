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
    public abstract class AbstractTermsAndConditionsDao : AbstractBaseDao
    {
        
        public abstract PagedList<AbstractTermsAndConditions> TermsAndConditions_All(PageParam pageParam, string search);
        public abstract SuccessResult<AbstractTermsAndConditions> TermsAndConditions_ById(long Id);
        public abstract SuccessResult<AbstractTermsAndConditions> TermsAndConditions_Upsert(AbstractTermsAndConditions abstractTermsAndConditions);
    }
}
