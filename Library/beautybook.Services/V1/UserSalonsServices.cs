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
    public class UserSalonsServices : AbstractUserSalonsServices
    {
        private AbstractUserSalonsDao abstractUserSalonsDao;

        public UserSalonsServices(AbstractUserSalonsDao abstractUserSalonsDao)
        {
            this.abstractUserSalonsDao = abstractUserSalonsDao;
        }
        public override PagedList<AbstractUserSalons> UserSalons_gettodayworksheet(PageParam pageParam, string search, long UserId, long SalonId, string AttendanceDate)
        {
            return this.abstractUserSalonsDao.UserSalons_gettodayworksheet(pageParam, search, UserId, SalonId, AttendanceDate);
        }
    }

}