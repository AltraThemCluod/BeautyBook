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
    public class LookUpStateServices : AbstractLookUpStateServices
    {
        private AbstractLookUpStateDao abstractLookUpStateDao;

        public LookUpStateServices(AbstractLookUpStateDao abstractLookUpStateDao)
        {
            this.abstractLookUpStateDao = abstractLookUpStateDao;
        }
        public override PagedList<AbstractLookUpState> LookUpState_All(PageParam pageParam, string search, long CountryId)
        {
            return this.abstractLookUpStateDao.LookUpState_All(pageParam, search, CountryId);
        }
    }

}