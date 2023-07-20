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
    public abstract class AbstractSalonOwnerDashboardServices
    {
        public abstract SuccessResult<AbstractSalonOwnerSMSandEmailPackages> SalonOwnerSMSandEmailPackages_All(long SalonId);
        public abstract SuccessResult<AbstractTecnicalSupport> TecnicalSupport_SalonId(long SalonId);
        public abstract PagedList<AbstractSalonOwnerNewCustomer> SalonOwnerNewCustomer_All(PageParam pageParam, string search, long SalonId, string FromDate, string ToDate,long Type);
        public abstract PagedList<AbstractSalonOwnerNewCustomer> SalonOwnerMostRequestedEmployee_All(PageParam pageParam, string search, long SalonId, string FromDate, string ToDate, long Type);
        public abstract PagedList<AbstractSalonOwnerArray> SalonOwnerDashboard_All(PageParam pageParam, string search, long SalonId, string FromDate, string ToDate,long Type);
        public abstract PagedList<AbstractSalonOwnerTopRequestedServices> SalonOwnerTopRequestedServices_All(PageParam pageParam, string search, long SalonId, string ServiceIds, string FromDate, string ToDate, long Type);
    }
}
