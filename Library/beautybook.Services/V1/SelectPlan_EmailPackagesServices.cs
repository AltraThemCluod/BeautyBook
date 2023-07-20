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
    public class SelectPlan_EmailPackagesServices : AbstractSelectPlan_EmailPackagesServices
    {
        private AbstractSelectPlan_EmailPackagesDao abstractSelectPlan_EmailPackagesDao;

        public SelectPlan_EmailPackagesServices(AbstractSelectPlan_EmailPackagesDao abstractSelectPlan_EmailPackagesDao)
        {
            this.abstractSelectPlan_EmailPackagesDao = abstractSelectPlan_EmailPackagesDao;
        }

        public override SuccessResult<AbstractSelectPlan_EmailPackages> SelectPlan_EmailPackages_Upsert(AbstractSelectPlan_EmailPackages abstractSelectPlan_EmailPackages)
        {
            return this.abstractSelectPlan_EmailPackagesDao.SelectPlan_EmailPackages_Upsert(abstractSelectPlan_EmailPackages);
        }

        public override SuccessResult<AbstractSelectPlan_EmailPackages> SelectPlanEmailPackages_UpdatePaymentStatus(long SelectPlanEmailPackagesId)
        {
            return this.abstractSelectPlan_EmailPackagesDao.SelectPlanEmailPackages_UpdatePaymentStatus(SelectPlanEmailPackagesId);
        }

        public override PagedList<AbstractSelectPlan_EmailPackages> SelectPlan_EmailPackages_BySalonId(PageParam pageParam, string search, long SalonId)
        {
            return this.abstractSelectPlan_EmailPackagesDao.SelectPlan_EmailPackages_BySalonId(pageParam, search, SalonId);

        }
    }
}
