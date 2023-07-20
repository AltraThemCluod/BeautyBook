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
    public abstract class AbstractCreate_EmailMsg_PackagesDao
    {
        public abstract SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_Upsert(AbstractCreate_EmailMsg_Packages abstractCreate_EmailMsg_Packages);
        public abstract SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_ById(long Id);
        public abstract PagedList<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_All(PageParam pageParam, string search, long LookUpDurationId,long SalonId);
        public abstract SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_ActInAct(long Id);

    }
}
