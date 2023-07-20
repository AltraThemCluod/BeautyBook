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
    public class LookUpPaymentMethodServices : AbstractLookUpPaymentMethodServices
    {
        private AbstractLookUpPaymentMethodDao abstractLookUpPaymentMethodDao;

        public LookUpPaymentMethodServices(AbstractLookUpPaymentMethodDao abstractLookUpPaymentMethodDao)
        {
            this.abstractLookUpPaymentMethodDao = abstractLookUpPaymentMethodDao;
        }
        public override PagedList<AbstractLookUpPaymentMethod> LookUpPaymentMethod_All(PageParam pageParam, string search)
        {
            return this.abstractLookUpPaymentMethodDao.LookUpPaymentMethod_All(pageParam, search);
        }
    }

}