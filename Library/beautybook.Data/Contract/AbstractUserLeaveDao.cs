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
    public abstract class AbstractUserLeaveDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractUserLeave> UserLeave_All(PageParam pageParam, string search , AbstractUserLeave abstractUserLeave);
        public abstract SuccessResult<AbstractUserLeave> UserLeave_ById(long Id);
        public abstract SuccessResult<AbstractUserLeave> UserLeave_Upsert(AbstractUserLeave abstractUserLeave);
        public abstract SuccessResult<AbstractUserLeave> UserLeave_ChangeStatus(long Id, long LookUpStatusId, long LookUpStatusChangedBy);
        public abstract SuccessResult<AbstractUserLeave> UserLeave_Delete(long Id, long DeletedBy);
        public abstract PagedList<AbstractUserLeave> UserLeave_LeaveTypeCount(PageParam pageParam, AbstractUserLeave abstractUserLeave);
    }
}
