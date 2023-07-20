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
    public class SelectPlan_SMSPackagesServices : AbstractSelectPlan_SMSPackagesServices
    {
        private AbstractSelectPlan_SMSPackagesDao abstractSelectPlan_SMSPackagesDao;

        public SelectPlan_SMSPackagesServices(AbstractSelectPlan_SMSPackagesDao abstractSelectPlan_SMSPackagesDao)
        {
            this.abstractSelectPlan_SMSPackagesDao = abstractSelectPlan_SMSPackagesDao;
        }

        public override SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlan_SMSPackages_Upsert(AbstractSelectPlan_SMSPackages abstractSelectPlan_SMSPackages)
        {
            return this.abstractSelectPlan_SMSPackagesDao.SelectPlan_SMSPackages_Upsert(abstractSelectPlan_SMSPackages);
        }

        public override SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlanSMSPackages_UpdatePaymentStatus(long SelectPlanSMSPackagesId)
        {
            return this.abstractSelectPlan_SMSPackagesDao.SelectPlanSMSPackages_UpdatePaymentStatus(SelectPlanSMSPackagesId);
        }

        public override SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlan_CompleteNoOfMsg(long Number,long SalonId,long IsSms)
        {
            return this.abstractSelectPlan_SMSPackagesDao.SelectPlan_CompleteNoOfMsg(Number, SalonId, IsSms);
        }



        public override PagedList<AbstractSelectPlan_SMSPackages> SelectPlan_SMSPackages_BySalonId(PageParam pageParam, string search, long SalonId)
        {
            return this.abstractSelectPlan_SMSPackagesDao.SelectPlan_SMSPackages_BySalonId(pageParam, search, SalonId);
        }
    }
}
