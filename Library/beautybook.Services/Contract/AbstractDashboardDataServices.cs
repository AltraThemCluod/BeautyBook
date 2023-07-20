using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;

namespace BeautyBook.Services.Contract
{
    public abstract class AbstractDashboardDataServices
    {
        public abstract DashboardData DashboardData_All(PageParam pageParam, string search,long LookUpCountry,long LookUpState);
    }
}
