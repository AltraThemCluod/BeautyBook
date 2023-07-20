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
    public class LookUpUserTypeServices : AbstractLookUpUserTypeServices
    {
        private AbstractLookUpUserTypeDao abstractLookUpUserTypeDao;

        public LookUpUserTypeServices(AbstractLookUpUserTypeDao abstractLookUpUserTypeDao)
        {
            this.abstractLookUpUserTypeDao = abstractLookUpUserTypeDao;
        }
        public override PagedList<AbstractLookUpUserType> LookUpUserType_All(PageParam pageParam, string search)
        {
            return this.abstractLookUpUserTypeDao.LookUpUserType_All(pageParam, search);
        }
    }

}