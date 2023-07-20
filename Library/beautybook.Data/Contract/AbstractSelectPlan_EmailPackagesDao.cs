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
    public abstract class AbstractSelectPlan_EmailPackagesDao
    {
        public abstract SuccessResult<AbstractSelectPlan_EmailPackages> SelectPlan_EmailPackages_Upsert(AbstractSelectPlan_EmailPackages abstractSelectPlan_EmailPackages);
        public abstract SuccessResult<AbstractSelectPlan_EmailPackages> SelectPlanEmailPackages_UpdatePaymentStatus(long SelectPlanEmailPackagesId);
        public abstract PagedList<AbstractSelectPlan_EmailPackages> SelectPlan_EmailPackages_BySalonId(PageParam pageParam, string search, long SalonId);

    }
}
