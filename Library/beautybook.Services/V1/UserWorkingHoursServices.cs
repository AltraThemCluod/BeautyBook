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
    public class UserWorkingHoursServices : AbstractUserWorkingHoursServices
    {
        private AbstractUserWorkingHoursDao abstractUserWorkingHoursDao;

        public UserWorkingHoursServices(AbstractUserWorkingHoursDao abstractUserWorkingHoursDao)
        {
            this.abstractUserWorkingHoursDao = abstractUserWorkingHoursDao;
        }
        public override PagedList<AbstractUserWorkingHours> UserWorkingHours_ByUserId(PageParam pageParam, string search,long UserId)
        {
            return this.abstractUserWorkingHoursDao.UserWorkingHours_ByUserId(pageParam, search, UserId);
        }

        public override PagedList<AbstractUserWorkingHours> EmployeeWorkingHour_ServicesId(PageParam pageParam, string search, long CategoryId, string ServicesIds, long SalonId)
        {
            return this.abstractUserWorkingHoursDao.EmployeeWorkingHour_ServicesId(pageParam, search, CategoryId, ServicesIds, SalonId);
        }


    }

}