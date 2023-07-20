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
    public class TermsAndConditionsServices : AbstractTermsAndConditionsServices
    {
        private AbstractTermsAndConditionsDao abstractTermsAndConditionsDao;

        public TermsAndConditionsServices(AbstractTermsAndConditionsDao abstractTermsAndConditionsDao)
        {
            this.abstractTermsAndConditionsDao = abstractTermsAndConditionsDao;
        }

        public override PagedList<AbstractTermsAndConditions> TermsAndConditions_All(PageParam pageParam, string search)
        {
            return this.abstractTermsAndConditionsDao.TermsAndConditions_All(pageParam, search);
        }

       
        public override SuccessResult<AbstractTermsAndConditions> TermsAndConditions_ById(long Id)
        {
            return this.abstractTermsAndConditionsDao.TermsAndConditions_ById(Id);
        }
        public override SuccessResult<AbstractTermsAndConditions> TermsAndConditions_Upsert(AbstractTermsAndConditions abstractTermsAndConditions)
        {
            return this.abstractTermsAndConditionsDao.TermsAndConditions_Upsert(abstractTermsAndConditions); ;
        }
        
    }

}