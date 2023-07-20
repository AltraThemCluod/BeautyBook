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
    public class LookUpLeaveTypeServices : AbstractLookUpLeaveTypeServices
    {
        private AbstractLookUpLeaveTypeDao abstractLookUpLeaveTypeDao;

        public LookUpLeaveTypeServices(AbstractLookUpLeaveTypeDao abstractLookUpLeaveTypeDao)
        {
            this.abstractLookUpLeaveTypeDao = abstractLookUpLeaveTypeDao;
        }
        public override PagedList<AbstractLookUpLeaveType> LookUpLeaveType_All(PageParam pageParam, string search)
        {
            return this.abstractLookUpLeaveTypeDao.LookUpLeaveType_All(pageParam, search);
        }
    }

}