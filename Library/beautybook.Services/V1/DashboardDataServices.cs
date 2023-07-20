using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;

namespace BeautyBook.Services.V1
{
    public class DashboardDataServices : AbstractDashboardDataServices
    {
        private AbstractDashboardDataDao abstractDashboardDataDao;

        public DashboardDataServices(AbstractDashboardDataDao abstractDashboardDataDao)
        {
            this.abstractDashboardDataDao = abstractDashboardDataDao;
        }

        public override DashboardData DashboardData_All(PageParam pageParam, string search, long LookUpCountry,long LookUpState)
        {
            return this.abstractDashboardDataDao.DashboardData_All(pageParam, search, LookUpCountry, LookUpState);
        }
    }

}