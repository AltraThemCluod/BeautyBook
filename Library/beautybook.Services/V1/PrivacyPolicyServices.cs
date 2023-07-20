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
    public class PrivacyPolicyServices : AbstractPrivacyPolicyServices
    {
        private AbstractPrivacyPolicyDao abstractPrivacyPolicyDao;

        public PrivacyPolicyServices(AbstractPrivacyPolicyDao abstractPrivacyPolicyDao)
        {
            this.abstractPrivacyPolicyDao = abstractPrivacyPolicyDao;
        }

       
        public override SuccessResult<AbstractPrivacyPolicy> PrivacyPolicy_ById(long Id , long Type)
        {
            return this.abstractPrivacyPolicyDao.PrivacyPolicy_ById(Id, Type);
        }

        public override SuccessResult<AbstractPrivacyPolicy> PrivacyPolicy_Upsert(AbstractPrivacyPolicy abstractPrivacyPolicy)
        {
            return this.abstractPrivacyPolicyDao.PrivacyPolicy_Upsert(abstractPrivacyPolicy); ;
        }
    }
}