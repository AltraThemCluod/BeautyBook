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
    public class Create_SMS_PackagesServices : AbstractCreate_SMS_PackagesServices
    {
        private AbstractCreate_SMS_PackagesDao abstractCreate_SMS_PackagesDao;

        public Create_SMS_PackagesServices(AbstractCreate_SMS_PackagesDao abstractCreate_SMS_PackagesDao)
        {
            this.abstractCreate_SMS_PackagesDao = abstractCreate_SMS_PackagesDao;
        }

        public override SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_Upsert(AbstractCreate_SMS_Packages abstractCreate_SMS_Packages)
        {
            return this.abstractCreate_SMS_PackagesDao.Create_SMS_Packages_Upsert(abstractCreate_SMS_Packages);
        }

        //public override SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_ById(long Id)
        //{
        //    return this.abstractCreate_SMS_PackagesDao.Create_SMS_Packages_ById(Id);
        //}

        public override SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_ById(long Id)
        {
            return this.abstractCreate_SMS_PackagesDao.Create_SMS_Packages_ById(Id);
        }

        public override PagedList<AbstractCreate_SMS_Packages> Create_SMS_Packages_All(PageParam pageParam, string search, long LookUpDurationId,long SalonId)
        {
            return this.abstractCreate_SMS_PackagesDao.Create_SMS_Packages_All(pageParam, search, LookUpDurationId, SalonId);

        }
        public override SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_ActInAct(long Id)
        {
            return this.abstractCreate_SMS_PackagesDao.Create_SMS_Packages_ActInAct(Id);
        }

        public override SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_Delete(long Id, long DeletedBy)
        {
            return this.abstractCreate_SMS_PackagesDao.Create_SMS_Packages_Delete(Id, DeletedBy);
        }


    }
}
