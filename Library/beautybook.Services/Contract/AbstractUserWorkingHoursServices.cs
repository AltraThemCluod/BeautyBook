using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Services.Contract
{
    public abstract class AbstractUserWorkingHoursServices
    {
        public abstract PagedList<AbstractUserWorkingHours> UserWorkingHours_ByUserId(PageParam pageParam, string search, long UserId);
        public abstract PagedList<AbstractUserWorkingHours> EmployeeWorkingHour_ServicesId(PageParam pageParam, string search, long CategoryId, string ServicesIds, long SalonId);

    }
}
