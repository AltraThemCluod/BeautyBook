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
    public class LookUpEmployeeTypeServices : AbstractLookUpEmployeeTypeServices
    {
        private AbstractLookUpEmployeeTypeDao abstractLookUpEmployeeTypeDao;

        public LookUpEmployeeTypeServices(AbstractLookUpEmployeeTypeDao abstractLookUpEmployeeTypeDao)
        {
            this.abstractLookUpEmployeeTypeDao = abstractLookUpEmployeeTypeDao;
        }
        public override PagedList<AbstractLookUpEmployeeType> LookUpEmployeeType_All(PageParam pageParam, string search)
        {
            return this.abstractLookUpEmployeeTypeDao.LookUpEmployeeType_All(pageParam, search);
        }
    }

}