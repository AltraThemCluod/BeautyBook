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
    public abstract class AbstractLookUpDurationDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractLookUpDuration> LookUpDuration_All(PageParam pageParam, string search);
    }
}
