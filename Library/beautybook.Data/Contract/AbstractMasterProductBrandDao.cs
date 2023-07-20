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
    public abstract class AbstractMasterProductBrandDao
    {
        public abstract SuccessResult<AbstractMasterProductBrand> MasterProductBrand_Upsert(AbstractMasterProductBrand abstractMasterProductBrand);
        public abstract SuccessResult<AbstractMasterProductBrand> MasterProductBrand_ById(long Id);
        public abstract PagedList<AbstractMasterProductBrand> MasterProductBrand_All(PageParam pageParam, string search, long SalonId);
        public abstract SuccessResult<AbstractMasterProductBrand> MasterProductBrand_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractMasterProductBrand> MasterProductBrand_ActInAct(long Id);

    }
}
