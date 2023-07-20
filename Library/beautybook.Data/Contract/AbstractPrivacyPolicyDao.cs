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
    public abstract class AbstractPrivacyPolicyDao : AbstractBaseDao
    {
        public abstract SuccessResult<AbstractPrivacyPolicy> PrivacyPolicy_ById(long Id , long Type);
        public abstract SuccessResult<AbstractPrivacyPolicy> PrivacyPolicy_Upsert(AbstractPrivacyPolicy abstractPrivacyPolicy);
    }
}
