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
    public abstract class AbstractSalonsServices
    {
        public abstract PagedList<AbstractSalons> Salons_All(PageParam pageParam, string search, long LookUpStatusId);
        public abstract PagedList<AbstractSalons> Salons_ByUserId(PageParam pageParam, string search, long UserId , long LookUpStatusId);
        public abstract SuccessResult<AbstractSalons> Salons_ById(long Id);
        public abstract SuccessResult<AbstractSalons> Salons_Upsert(AbstractSalons abstractSalons);
        public abstract SuccessResult<AbstractSalons> Salons_ActInact(long Id,long LookUpStatusId, long LookUpStatusChangedBy);
        public abstract SuccessResult<AbstractSalons> Salons_ApprovedUnApproved(long Id,long LookUpStatusId,long LookUpStatusChangedBy);
        public abstract SuccessResult<AbstractSalons> Salons_Delete(long Id, long DeletedBy);
    }
}
