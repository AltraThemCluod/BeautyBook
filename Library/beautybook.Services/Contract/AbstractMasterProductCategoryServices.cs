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
    public abstract class AbstractMasterProductCategoryServices
    {
        public abstract SuccessResult<AbstractMasterProductCategory> MasterProductCategory_Upsert(AbstractMasterProductCategory abstractMasterProductCategory);
        public abstract SuccessResult<AbstractMasterProductCategory> MasterProductCategory_ById(long Id);
        public abstract PagedList<AbstractMasterProductCategory> MasterProductCategory_All(PageParam pageParam, string search, long ProductTypeId);
        public abstract SuccessResult<AbstractMasterProductCategory> MasterProductCategory_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractMasterProductCategory> MasterProductCategory_ActInact(long Id);
    }
}
