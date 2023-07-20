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
    public abstract class AbstractCreate_SMS_PackagesDao
    {
        public abstract SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_Upsert(AbstractCreate_SMS_Packages abstractCreate_SMS_Packages);
        public abstract SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_ById(long Id);
        public abstract PagedList<AbstractCreate_SMS_Packages> Create_SMS_Packages_All(PageParam pageParam, string search, long LookUpDurationId,long SalonId);
        public abstract SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_ActInAct(long Id);

    }
}
