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
    public abstract class AbstractUserWorkSheetDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractUserWorkSheet> UserWorkSheet_All(PageParam pageParam, string search , AbstractUserWorkSheet abstractUserWorkSheet);
        public abstract SuccessResult<AbstractUserWorkSheet> UserWorkSheet_ById(long Id);
        public abstract SuccessResult<AbstractUserWorkSheet> UserWorkSheet_Upsert(AbstractUserWorkSheet abstractUserWorkSheet);
    }
}
