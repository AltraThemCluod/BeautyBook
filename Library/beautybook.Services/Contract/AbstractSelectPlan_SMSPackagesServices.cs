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
    public abstract class AbstractSelectPlan_SMSPackagesServices
    {
        public abstract SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlan_SMSPackages_Upsert(AbstractSelectPlan_SMSPackages abstractSelectPlan_SMSPackages);
        public abstract SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlan_CompleteNoOfMsg(long Number,long SalonId,long IsSms);
        public abstract SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlanSMSPackages_UpdatePaymentStatus(long SelectPlanSMSPackagesId);

        public abstract PagedList<AbstractSelectPlan_SMSPackages> SelectPlan_SMSPackages_BySalonId(PageParam pageParam, string search, long SalonId);

    }
}


















