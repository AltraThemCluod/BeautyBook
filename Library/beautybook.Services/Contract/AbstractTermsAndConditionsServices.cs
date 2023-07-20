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
    public abstract class AbstractTermsAndConditionsServices
    {
       
        public abstract PagedList<AbstractTermsAndConditions> TermsAndConditions_All(PageParam pageParam, string search);
        public abstract SuccessResult<AbstractTermsAndConditions> TermsAndConditions_ById(long Id);
        public abstract SuccessResult<AbstractTermsAndConditions> TermsAndConditions_Upsert(AbstractTermsAndConditions abstractTermsAndConditions);
    }
}
