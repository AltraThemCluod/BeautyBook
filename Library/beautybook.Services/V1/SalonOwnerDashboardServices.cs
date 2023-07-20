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
    public class SalonOwnerDashboardServices : AbstractSalonOwnerDashboardServices
    {
        private AbstractSalonOwnerDashboardDao abstractSalonOwnerDashboardDao;

        public SalonOwnerDashboardServices(AbstractSalonOwnerDashboardDao abstractSalonOwnerDashboardDao)
        {
            this.abstractSalonOwnerDashboardDao = abstractSalonOwnerDashboardDao;
        }

        public override SuccessResult<AbstractSalonOwnerSMSandEmailPackages> SalonOwnerSMSandEmailPackages_All(long SalonId)
        {
            return this.abstractSalonOwnerDashboardDao.SalonOwnerSMSandEmailPackages_All(SalonId);
        }

        public override SuccessResult<AbstractTecnicalSupport> TecnicalSupport_SalonId(long SalonId)
        {
            return this.abstractSalonOwnerDashboardDao.TecnicalSupport_SalonId(SalonId);
        }

        public override PagedList<AbstractSalonOwnerNewCustomer> SalonOwnerNewCustomer_All(PageParam pageParam, string search, long SalonId, string FromDate, string ToDate,long Type)
        {
            return this.abstractSalonOwnerDashboardDao.SalonOwnerNewCustomer_All(pageParam, search, SalonId, FromDate, ToDate,Type);
        }

        public override PagedList<AbstractSalonOwnerNewCustomer> SalonOwnerMostRequestedEmployee_All(PageParam pageParam, string search, long SalonId, string FromDate, string ToDate, long Type)
        {
            return this.abstractSalonOwnerDashboardDao.SalonOwnerMostRequestedEmployee_All(pageParam, search, SalonId, FromDate, ToDate, Type);
        }
        public override PagedList<AbstractSalonOwnerArray> SalonOwnerDashboard_All(PageParam pageParam, string search, long SalonId, string FromDate, string ToDate,long Type)
        {
            return this.abstractSalonOwnerDashboardDao.SalonOwnerDashboard_All(pageParam, search, SalonId, FromDate, ToDate, Type);
        }

        public override PagedList<AbstractSalonOwnerTopRequestedServices> SalonOwnerTopRequestedServices_All(PageParam pageParam, string search, long SalonId,string ServiceIds, string FromDate, string ToDate, long Type)
        {
            return this.abstractSalonOwnerDashboardDao.SalonOwnerTopRequestedServices_All(pageParam, search, SalonId, ServiceIds, FromDate, ToDate, Type);
        }

    }

}