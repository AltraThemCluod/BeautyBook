using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;

namespace BeautyBook.Services.V1
{
    public class MasterProductTypeServices : AbstractMasterProductTypeServices
    {
        private AbstractMasterProductTypeDao abstractMasterProductTypeDao;

        public MasterProductTypeServices(AbstractMasterProductTypeDao abstractMasterProductTypeDao)
        {
            this.abstractMasterProductTypeDao = abstractMasterProductTypeDao;
        }

        public override SuccessResult<AbstractMasterProductType> MasterProductType_Upsert(AbstractMasterProductType abstractMasterProductType)
        {
            return this.abstractMasterProductTypeDao.MasterProductType_Upsert(abstractMasterProductType);
        }

        public override SuccessResult<AbstractMasterProductType> MasterProductType_ById(long Id)
        {
            return this.abstractMasterProductTypeDao.MasterProductType_ById(Id);
        }

        public override PagedList<AbstractMasterProductType> MasterProductType_All(PageParam pageParam, string search, bool IsAllow)
        {
            return this.abstractMasterProductTypeDao.MasterProductType_All(pageParam, search, IsAllow);

        }
        public override SuccessResult<AbstractMasterProductType> MasterProductType_ActInAct(long Id)
        {
            return this.abstractMasterProductTypeDao.MasterProductType_ActInAct(Id);
        }

        public override SuccessResult<AbstractMasterProductType> MasterProductType_Delete(long Id, long DeletedBy)
        {
            return this.abstractMasterProductTypeDao.MasterProductType_Delete(Id, DeletedBy);
        }

    }
}
