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
    public abstract class AbstractLookUpServicesDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractLookUpServices> LookUpServices_All(PageParam pageParam, string search, long ParentId,long SalonId,long UserId);
        public abstract PagedList<AbstractLookUpServices> CustomCategory_All(PageParam pageParam, string search, long ParentId,long SalonId,long UserId);
        public abstract PagedList<AbstractLookUpServices> CustomServices_All(PageParam pageParam, string search,long SalonId,long UserId);
        public abstract PagedList<AbstractLookUpServices> LookUpServices_ParentId(PageParam pageParam, string search, AbstractLookUpServices abstractLookUpServices);
        public abstract SuccessResult<AbstractLookUpServices> LookUpServices_ById(long Id);
        public abstract SuccessResult<AbstractLookUpServices> LookUpServices_Upsert(AbstractLookUpServices abstractLookUpServices);
        public abstract SuccessResult<AbstractLookUpServices> LookUpServices_ActInact(long Id,long LookUpStatusId,long LookUpStatusChangedBy);
        public abstract SuccessResult<AbstractLookUpServices> LookUpServices_Delete(long Id, long DeletedBy);
        public abstract PagedList<AbstractLookUpServices> LookUpServices_AllServices(long SalonId, long PackageId);

    }
}
