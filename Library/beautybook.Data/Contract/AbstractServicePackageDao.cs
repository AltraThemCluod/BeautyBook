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
    public abstract class AbstractServicePackageDao : AbstractBaseDao
    {
        public abstract SuccessResult<AbstractServicePackage> ServicePackage_ActInAct(long Id);
        public abstract PagedList<AbstractServicePackage> ServicePackage_All(PageParam pageParam, string search,long SalonId);
        public abstract SuccessResult<AbstractServicePackage> ServicePackage_ById(long Id);
        public abstract SuccessResult<AbstractServicePackage> ServicePackage_Upsert(AbstractServicePackage abstractServicePackage);
    }
}
