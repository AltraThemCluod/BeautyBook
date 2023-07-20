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
    public class UserLeaveServices : AbstractUserLeaveServices
    {
        private AbstractUserLeaveDao abstractUserLeaveDao;

        public UserLeaveServices(AbstractUserLeaveDao abstractUserLeaveDao)
        {
            this.abstractUserLeaveDao = abstractUserLeaveDao;
        }
        public override PagedList<AbstractUserLeave> UserLeave_All(PageParam pageParam, string search, AbstractUserLeave abstractUserLeave)
        {
            return this.abstractUserLeaveDao.UserLeave_All(pageParam, search, abstractUserLeave);
        }
        public override SuccessResult<AbstractUserLeave> UserLeave_ChangeStatus(long Id,long LookUpStatusId,long LookUpStatusChangedBy)
        {
            return this.abstractUserLeaveDao.UserLeave_ChangeStatus(Id, LookUpStatusId, LookUpStatusChangedBy);
        }
        public override SuccessResult<AbstractUserLeave> UserLeave_ById(long Id)
        {
            return this.abstractUserLeaveDao.UserLeave_ById(Id);
        }
        public override SuccessResult<AbstractUserLeave> UserLeave_Upsert(AbstractUserLeave abstractUserLeave)
        {
            return this.abstractUserLeaveDao.UserLeave_Upsert(abstractUserLeave);
        }
        public override SuccessResult<AbstractUserLeave> UserLeave_Delete(long Id, long DeletedBy)
        {
            return this.abstractUserLeaveDao.UserLeave_Delete(Id, DeletedBy);
        }
        public override PagedList<AbstractUserLeave> UserLeave_LeaveTypeCount(PageParam pageParam, AbstractUserLeave abstractUserLeave)
        {
            return this.abstractUserLeaveDao.UserLeave_LeaveTypeCount(pageParam, abstractUserLeave);
        }
    }

}