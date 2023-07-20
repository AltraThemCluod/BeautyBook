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
    public abstract class AbstractUserSalonsDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractUserSalons> UserSalons_gettodayworksheet(PageParam pageParam, string search ,long UserId,long SalonId,string AttendanceDate);
    }
}
