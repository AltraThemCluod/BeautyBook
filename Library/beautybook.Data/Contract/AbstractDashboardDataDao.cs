using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;

namespace BeautyBook.Data.Contract
{
    public abstract class AbstractDashboardDataDao : AbstractBaseDao
    {
        public abstract DashboardData DashboardData_All(PageParam pageParam, string search,long LookUpCountry,long LookUpState);
    }
}
