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
    public class LookUpDurationServices : AbstractLookUpDurationServices
    {
        private AbstractLookUpDurationDao abstractLookUpDurationDao;

        public LookUpDurationServices(AbstractLookUpDurationDao abstractLookUpDurationDao)
        {
            this.abstractLookUpDurationDao = abstractLookUpDurationDao;
        }
        public override PagedList<AbstractLookUpDuration> LookUpDuration_All(PageParam pageParam, string search)
        {
            return this.abstractLookUpDurationDao.LookUpDuration_All(pageParam, search);
        }
    }

}