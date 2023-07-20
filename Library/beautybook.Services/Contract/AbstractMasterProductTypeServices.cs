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
    public abstract class AbstractMasterProductTypeServices
    {
        public abstract SuccessResult<AbstractMasterProductType> MasterProductType_Upsert(AbstractMasterProductType abstractMasterProductType);
        public abstract SuccessResult<AbstractMasterProductType> MasterProductType_ById(long Id);
        public abstract PagedList<AbstractMasterProductType> MasterProductType_All(PageParam pageParam, string search, bool IsAllow);
        public abstract SuccessResult<AbstractMasterProductType> MasterProductType_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractMasterProductType> MasterProductType_ActInAct(long Id);

    }
}
