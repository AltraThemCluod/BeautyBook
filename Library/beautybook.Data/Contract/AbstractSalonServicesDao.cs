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
    public abstract class AbstractSalonServicesDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractSalonServices> SalonServices_All(PageParam pageParam, string search, long SalonsId, long LookUpServicesId, long LookUpStatusId , long LookUpCategoryId);
        public abstract SuccessResult<AbstractSalonServices> SalonServices_ById(long Id);
        public abstract SuccessResult<AbstractSalonServices> SalonServices_Upsert(AbstractSalonServices abstractSalonServices);
        public abstract SuccessResult<AbstractSalonServices> BlankSalonServices_Upsert(AbstractSalonServices abstractSalonServices);
        public abstract SuccessResult<AbstractSalonServices> SalonServices_ActInact(long Id, long LookUpStatusId, long SalonId, long LookUpStatusChangedBy);
        public abstract SuccessResult<AbstractSalonServices> SalonServices_Delete(long Id, long DeletedBy);
    }
}
